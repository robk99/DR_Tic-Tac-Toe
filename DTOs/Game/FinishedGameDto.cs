using DR_Tic_Tac_Toe.Enums;

namespace DR_Tic_Tac_Toe.DTOs.Game
{
    public record FinishedGameDto
    {
        public GameIcons? Winner { get; set; }
        public bool GameFinished { get; set; }
    }
}
