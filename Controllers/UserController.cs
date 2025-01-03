using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.Common.Errors;
using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.DTOs;
using DR_Tic_Tac_Toe.DTOs.Requests;
using DR_Tic_Tac_Toe.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DR_Tic_Tac_Toe.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly TokenService _tokenService;
        private readonly HashingService _hashingService;
        private readonly UserRepository _userRepository;
        private readonly UserMapper _userMapper;

        public UserController(TokenService tokenService, HashingService hashingService, UserRepository userRepository, UserMapper userMapper)
        {
            _tokenService = tokenService;
            _hashingService = hashingService;
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (await _userRepository.GetByUsername(request.Username) != null)
            {
                return Conflict(UserErrors.EmailNotUnique(request.Username));
            }

            var user = _userMapper.FromRegistrationRequestToModel(request);
            await _userRepository.Add(user);

            return Created("", new { id = user.Id });
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetByUsername(request.Username);
            if (user == null) return Unauthorized(UserErrors.NotFoundByEmail(request.Username));

            bool verified = _hashingService.Verify(request.Password, user.PasswordHash);
            if (!verified) return Unauthorized(UserErrors.WrongPassword(request.Username));

            string token = _tokenService.CreateToken(user);

            return Created("", new { token = token });
        }

        [Authorize]
        [HttpGet("get-details/{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetDetails(int id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var userDetails = await _userRepository.GetDetails(id);
            if (userDetails == null) return NotFound(null);
            
            return Ok(userDetails);
        }
    }
}
