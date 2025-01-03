using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.DTOs.Game;
using DR_Tic_Tac_Toe.DTOs.Game.Requests;
using DR_Tic_Tac_Toe.Mappers;
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

        public GameController(GameRepository gameRepository, GameMapper gameMapper)
        {
            _gameRepository = gameRepository;
            _gameMapper = gameMapper;
        }

        [Authorize]
        [HttpGet("get-games")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames([FromQuery] GamesFilteredRequest request)
        {
            // TODO: Add fluent validation for mandatory params

            var games = await _gameRepository.GetGamesFiltered(request);
            
            return Ok(games);
        }


        [Authorize]
        [HttpGet("get-details/{id}")]
        public async Task<ActionResult<GameDetailsDto>> GetDetails(int id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var game = await _gameRepository.GetDetails(id);
            if (game == null) return NotFound(null);

            var gameDetails = _gameMapper.FromModelToGameDetails(game);

            return Ok(gameDetails);
        }

        [HttpPost("start-new")]
        [ServiceFilter(typeof(ValidateUserFilter))]
        public async Task<ActionResult> StartNew([FromBody] CreateNewGameRequest request)
        {
            // TODO: Add fluent validation for just 1-9 Fields

            request.UserId = (int)HttpContext.Items["UserId"];

            var game = _gameMapper.CreateNewGame(request);

            var created = await _gameRepository.Create(game);

            return Created("", new { created });
        }
    }
}
