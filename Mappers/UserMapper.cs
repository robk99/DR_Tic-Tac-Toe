using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.DTOs.Authentication.Requests;
using DR_Tic_Tac_Toe.Models;

namespace DR_Tic_Tac_Toe.Mappers
{
    public class UserMapper
    {
        private readonly HashingService _hashingService;

        public UserMapper(HashingService hashingService)
        {
            _hashingService = hashingService;
        }

        public User FromRegistrationRequestToModel(AuthenticationRequest request)
        {
            return new User
            {
                Username = request.Username,
                PasswordHash = _hashingService.Hash(request.Password)
            };
        }
    }

}
