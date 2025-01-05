using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.Common.Errors;
using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.DTOs.Game;
using DR_Tic_Tac_Toe.DTOs.Game.Requests;
using DR_Tic_Tac_Toe.DTOs.Game.Responses;
using DR_Tic_Tac_Toe.Enums;
using DR_Tic_Tac_Toe.Mappers;
using DR_Tic_Tac_Toe.Utils;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DR_Tic_Tac_Toe.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : Controller
    {
        private readonly GameRepository _gameRepository;
        private readonly GameMapper _gameMapper;
        private readonly IValidator<GamesFilteredRequest> _gamesFilteredRequestValidator;
        private readonly IValidator<int> _identityValidator;
        private readonly IValidator<StartNewGameRequest> _startNewGameRequestValidator;
        private readonly IValidator<NewGameMoveRequest> _newGameMoveRequestValidator;

        public GameController(GameRepository gameRepository, GameMapper gameMapper,
            IValidator<GamesFilteredRequest> gamesFilteredRequestValidator, IValidator<int> identityValidator,
            IValidator<StartNewGameRequest> startNewGameRequestValidator,
            IValidator<NewGameMoveRequest> newGameMoveRequestValidator)
        {
            _gameRepository = gameRepository;
            _gameMapper = gameMapper;
            _gamesFilteredRequestValidator = gamesFilteredRequestValidator;
            _identityValidator = identityValidator;
            _startNewGameRequestValidator = startNewGameRequestValidator;
            _newGameMoveRequestValidator = newGameMoveRequestValidator;
        }

        [Authorize]
        [HttpGet("get-games")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames([FromQuery] GamesFilteredRequest request)
        {
            ValidationResult validationResult = await _gamesFilteredRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(ValidationErrors.BadRequest(validationResult.Errors));
            }

            var games = await _gameRepository.GetGamesFiltered(request);

            return Ok(games);
        }


        [Authorize]
        [HttpGet("get-details/{id}")]
        public async Task<ActionResult<GameDetailsDto>> GetDetails(int id)
        {
            ValidationResult validationResult = await _identityValidator.ValidateAsync(id);
            if (!validationResult.IsValid)
            {
                return BadRequest(ValidationErrors.BadRequest(validationResult.Errors));
            }

            var game = await _gameRepository.GetDetails(id);
            if (game == null) return NotFound(null);

            var gameDetails = _gameMapper.FromModelToGameDetails(game);

            return Ok(gameDetails);
        }

        [HttpPost("start-new")]
        [ServiceFilter(typeof(ValidateUserFilter))]
        public async Task<ActionResult> StartNew([FromBody] StartNewGameRequest request)
        {
            ValidationResult validationResult = await _startNewGameRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(ValidationErrors.BadRequest(validationResult.Errors));
            }

            var userId = (int)HttpContext.Items["UserId"];

            var game = _gameMapper.CreateNewGame(request, userId);

            var created = await _gameRepository.Create(game);
            if (!created) return Problem(DBErrors.CreateFailed().Message);

            return Created("", new { created });
        }


        [HttpPost("join-game")]
        [ServiceFilter(typeof(ValidateUserFilter))]
        public async Task<ActionResult<JoinGameResponse>> JoinGame([FromBody] JoinGameRequest request)
        {
            var userId = (int)HttpContext.Items["UserId"];

            var game = await _gameRepository.GetDetails(request.GameId);
            if (game == null)
                return NotFound(GameErrors.NotFoundById(request.GameId));

            if (game.Status == (int)GameStatus.Completed)
                return Conflict(GameErrors.GameFinished(request.GameId));

            if (game.Player1Id == userId || game.Player2Id == userId)
                return Conflict(GameErrors.YouAreAlreadyPlaying(request.GameId));

            if (game.Player2Id != null)
                return Conflict(GameErrors.MaxNumberOfPlayers(request.GameId));

            game.Player2Id = userId;
            game.Status = (int)GameStatus.InProgress;

            var updated = await _gameRepository.Update(game);
            if (!updated) return Problem(DBErrors.UpdateFailed().Message);
            
            var response = new JoinGameResponse()
            {
                Updated = updated,
                Message = "You joined the game!"
            };

            return Ok(new { updated });
        }


        [HttpPost("play-move")]
        [ServiceFilter(typeof(ValidateUserFilter))]
        public async Task<ActionResult<PlayMoveResponse>> PlayAMove([FromBody] NewGameMoveRequest request)
        {
            ValidationResult validationResult = await _newGameMoveRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(ValidationErrors.BadRequest(validationResult.Errors));
            }

            var playerId = (int)HttpContext.Items["UserId"];

            var game = await _gameRepository.GetDetails(request.GameId);
            if (game == null)
                return NotFound(GameErrors.NotFoundById(request.GameId));

            if (game.Player1Id != playerId && game.Player2Id != playerId)
                return Conflict(GameErrors.NotPartOfThisGame(request.GameId));

            if (game.Status == (int)GameStatus.Completed)
                return Conflict(GameErrors.GameFinished(request.GameId));

            if (!Gameutils.CheckIfItsPlayersTurn(playerId, game))
                return Conflict(GameErrors.NotYourTurn());

            GameIcons gameIcon = GameIcons.Empty;
            if (playerId == game.Player1Id) gameIcon = GameIcons.X;
            else gameIcon = GameIcons.O;

            var changeBoardDto = Gameutils.SetValueOnABoard(request.Field, game.BoardState, gameIcon);
            if (changeBoardDto.Error != null) return Conflict(changeBoardDto.Error);

            game.BoardState = changeBoardDto.NewBoardState;

            var response = new PlayMoveResponse();
            var gameFinishedDto = Gameutils.IsGameFinished(game.BoardState);
            if (gameFinishedDto.Winner != null)
            {
                game.WinnerId = gameFinishedDto.Winner == GameIcons.X ? game.Player1Id : game.Player2Id;
                if (playerId == game.WinnerId) response.Message = "You won!";
            }
            else if (gameFinishedDto.Status == GameStatus.Completed) response.Message = "It's a draw!";
            
            game.Status = (int)gameFinishedDto.Status;
            game.TurnCount++;

            response.Updated = await _gameRepository.Update(game);
            if (!response.Updated) return Problem(DBErrors.UpdateFailed().Message);

            return Ok(response);
        }
    }
}
