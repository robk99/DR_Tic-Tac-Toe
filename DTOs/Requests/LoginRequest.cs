namespace DR_Tic_Tac_Toe.DTOs.Requests
{
    public record LoginRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
