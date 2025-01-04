using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.Common.Errors;
using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.DTOs.Authentication.Requests;
using DR_Tic_Tac_Toe.DTOs.User;
using DR_Tic_Tac_Toe.Mappers;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<int> _identityValidator;

        public UserController(TokenService tokenService, HashingService hashingService, 
            UserRepository userRepository, UserMapper userMapper, IValidator<int> identityValidator)
        {
            _tokenService = tokenService;
            _hashingService = hashingService;
            _userRepository = userRepository;
            _userMapper = userMapper;
            _identityValidator = identityValidator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] AuthenticationRequest request)
        {
            if (await _userRepository.GetByUsername(request.Username) != null)
            {
                return Conflict(UserErrors.EmailNotUnique(request.Username));
            }

            var user = _userMapper.FromRegistrationRequestToModel(request);
            var created = await _userRepository.Add(user);
            if (!created) return Problem(UserErrors.RegisterFailed().Message);

            return Created("", new { created });
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthenticationRequest request)
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
            ValidationResult validationResult = await _identityValidator.ValidateAsync(id);
            if (!validationResult.IsValid)
            {
                return BadRequest(ValidationErrors.BadRequest(validationResult.Errors));
            }

            var userDetails = await _userRepository.GetDetails(id);
            if (userDetails == null) return NotFound(UserErrors.NotFoundById(id));
            
            return Ok(userDetails);
        }
    }
}
