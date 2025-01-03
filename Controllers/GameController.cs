using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.DTOs;
using DR_Tic_Tac_Toe.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DR_Tic_Tac_Toe.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : Controller
    {
        private readonly GameRepository _gameRepository;

        public GameController(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [Authorize]
        [HttpGet("get-games")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames([FromQuery] GamesFilteredRequest request)
        {
            // TODO: Add fluent validation for mandatory params

            var games = await _gameRepository.GetGamesFiltered(request);
            
            return Ok(games);
        }
    }
}
