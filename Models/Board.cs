using DR_Tic_Tac_Toe.Enums;

namespace DR_Tic_Tac_Toe.Models
{
    public class Board
    {
        public static string GetEmptyBoardString()
        {
            int[] boardNums  = new int[9];
            for (int i = 1; i <= 9; i++)
            {
                boardNums[i] = (int)GameIcons.Empty;
            }

            return boardNums.ToString();
        }
    }
}
