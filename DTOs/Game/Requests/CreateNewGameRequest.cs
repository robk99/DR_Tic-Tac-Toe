namespace DR_Tic_Tac_Toe.DTOs.Game.Requests
{
    public record CreateNewGameRequest
    {
        /// <summary>
        /// Number from 1 to 9 representing the field where the player wants to put the first X
        /// </summary>
        public int Field { get; set; }
        public int? UserId { get; set; }
    }
}
