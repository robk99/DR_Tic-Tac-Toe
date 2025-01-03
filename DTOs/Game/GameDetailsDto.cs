namespace DR_Tic_Tac_Toe.DTOs.Game
{
    /// <summary>
    /// Game with a detailed, more human readable board (3x3 array), next turn and status.
    /// </summary>
    public record GameDetailsDto
    {
        public List<List<string>> Board { get; set; }
        public string NextTurn { get; set; }
        public string Status { get; set; }
    }
}
