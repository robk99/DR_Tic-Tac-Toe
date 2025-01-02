using DR_Tic_Tac_Toe.Enums;

namespace DR_Tic_Tac_Toe.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int? Player1Id { get; set; }
        public int? Player2Id { get; set; }
        public int Status { get; set; } = (int)GameStatus.Open;
        public int TurnCount { get; set; } = 1;
        public string BoardState { get; set; } = Board.GetEmptyBoardString();
        public int? WinnerId { get; set; } = null;
    }

}
