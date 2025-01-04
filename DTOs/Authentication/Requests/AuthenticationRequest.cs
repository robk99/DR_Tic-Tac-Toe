namespace DR_Tic_Tac_Toe.DTOs.Authentication.Requests
{
    public record AuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
