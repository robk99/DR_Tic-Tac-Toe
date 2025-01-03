using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.DTOs;
using DR_Tic_Tac_Toe.DTOs.Requests;
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
    }
}
