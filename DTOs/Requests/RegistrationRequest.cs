namespace DR_Tic_Tac_Toe.DTOs.Requests
{
    public record RegistrationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
