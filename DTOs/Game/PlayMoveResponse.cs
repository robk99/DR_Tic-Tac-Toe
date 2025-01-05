namespace DR_Tic_Tac_Toe.DTOs.Game
{
    public record PlayMoveResponse
    {
        public bool Updated { get; set; }
        public string Message { get; set; }
    }
}
