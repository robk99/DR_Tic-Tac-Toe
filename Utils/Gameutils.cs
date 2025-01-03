using DR_Tic_Tac_Toe.Common.Errors;
using DR_Tic_Tac_Toe.DTOs.Game;
using DR_Tic_Tac_Toe.Enums;

namespace DR_Tic_Tac_Toe.Utils
{
    public static class Gameutils
    {
        /// <summary>
        /// Sets 1 or 2 on a field of the board from DB.
        /// </summary>
        /// <param name="field">From 1 to 9 (from "User's perspective")</param>
        /// <param name="boardState">Board from the DB, e.g. 120000000</param>
        /// <param name="iconValue">1 or 2 - states for the DB, throws an error on state 0 (Empty)</param>
        /// <returns>Returns new board or current board and an error if the field is already populated</returns>
        public static ChangeBoardDto SetValueOnABoard(int field, string boardState, GameIcons? iconValue = null)
        {
            var changedBoardDto = new ChangeBoardDto();

            char value;
            if (iconValue == null) value = ((int)GameIcons.X).ToString()[0];
            else if (iconValue == GameIcons.Empty) throw new Exception("Cannot set empty value on a field");
            else value = ((int)iconValue).ToString()[0];

            var boardStateArray = boardState.ToCharArray();

            if (boardStateArray[field - 1] - '0' != (int)GameIcons.Empty)
            {
                changedBoardDto.Error = GameErrors.FieldIsAlredyPopulated(field);
                changedBoardDto.NewBoardState = boardState;
            }
            else
            {
                boardStateArray[field - 1] = value;
                changedBoardDto.NewBoardState = new string(boardStateArray);
            }

            return changedBoardDto;
        }

        public static FinishedGameDto IsGameFinished(string boardState)
        {
            var finishedGameDto = new FinishedGameDto()
            {
                GameFinished = false
            };

            int[,] board = new int[3, 3];
            for (int i = 0; i < 9; i++)
            {
                board[i / 3, i % 3] = boardState[i] - '0';
            }

            // Check for a winner
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    finishedGameDto.GameFinished = true;
                    finishedGameDto.Winner = GameIcons.X;
                    break;
                }

                if (board[0, i] != 0 && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    finishedGameDto.GameFinished = false;
                    finishedGameDto.Winner = GameIcons.O;
                    break;
                }
            }

            if (finishedGameDto.GameFinished) return finishedGameDto;

            // Check diagonals
            if (board[0, 0] != 0 && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                finishedGameDto.GameFinished = true;
                finishedGameDto.Winner = GameIcons.X;
            }
            else if (board[0, 2] != 0 && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                finishedGameDto.GameFinished = false;
                finishedGameDto.Winner = GameIcons.O;
            }

            return finishedGameDto;
        }
    }
}
