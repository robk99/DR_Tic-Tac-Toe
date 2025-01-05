namespace DR_Tic_Tac_Toe.DTOs.Game.Responses
{
    public record BaseGameResponse
    {
        public bool Updated { get; set; }
        public string Message { get; set; }
    }
}
