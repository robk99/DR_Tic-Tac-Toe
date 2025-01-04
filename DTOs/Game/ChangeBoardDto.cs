using DR_Tic_Tac_Toe.Common.Errors;

namespace DR_Tic_Tac_Toe.DTOs.Game
{
    /// <summary>
    /// Returns the new board state after a move has been made, 
    /// and an error if there were validation problems
    /// </summary>
    public record ChangeBoardDto
    {
        public string NewBoardState { get; set; }
        public Error Error { get; set; }

    }
}
