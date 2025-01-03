namespace DR_Tic_Tac_Toe.DTOs.Game.Requests
{
    /// <summary>
    /// Join an already existing, non-full, non-completed game
    /// </summary>
    public record JoinGameRequest
    {
        public int GameId { get; set; }
    }
}
