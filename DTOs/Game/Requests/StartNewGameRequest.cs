namespace DR_Tic_Tac_Toe.DTOs.Game.Requests
{
    /// <summary>
    /// Used to start a new game or to play a move in an existing game
    /// </summary>
    public record StartNewGameRequest
    {
        /// <summary>
        /// Number from 1 to 9 representing the field where the player wants to put the first X
        /// </summary>
        public int Field { get; set; }
    }
}
