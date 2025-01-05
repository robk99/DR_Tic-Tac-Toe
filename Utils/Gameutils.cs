using DR_Tic_Tac_Toe.Common.Errors;
using DR_Tic_Tac_Toe.DTOs.Game;
using DR_Tic_Tac_Toe.Enums;
using DR_Tic_Tac_Toe.Models;

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
        public static ChangeBoardDto SetValueOnABoard(int field, string boardState, GameIcons iconValue)
        {
            var changedBoardDto = new ChangeBoardDto();

            char value = ((int)iconValue).ToString()[0];
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

        /// <summary>
        /// By BoardState gets the current game status and the winner if any.
        /// </summary>
        /// <param name="boardState"></param>
        /// <returns></returns>
        public static FinishedGameDto IsGameFinished(string boardState)
        {
            var finishedGameDto = new FinishedGameDto()
            {
                Status = GameStatus.InProgress
            };

            int[,] board = new int[3, 3];
            for (int i = 0; i < 9; i++)
            {
                board[i / 3, i % 3] = boardState[i] - '0';
            }

            // Check for a winner (horizontal and vertical)
            for (int i = 0; i < 3; i++)
            {
                var firstHorizontalValue = board[i, 0];
                if (firstHorizontalValue != 0 && firstHorizontalValue == board[i, 1] && firstHorizontalValue == board[i, 2])
                {
                    finishedGameDto.Status = GameStatus.Completed;
                    finishedGameDto.Winner = (GameIcons)firstHorizontalValue;
                    break;
                }

                var firstVerticalValue = board[0, i];
                if (firstVerticalValue != 0 && firstVerticalValue == board[1, i] && firstVerticalValue == board[2, i])
                {
                    finishedGameDto.Status = GameStatus.Completed;
                    finishedGameDto.Winner = (GameIcons)firstVerticalValue;
                    break;
                }
            }

            if (finishedGameDto.Status == GameStatus.Completed) return finishedGameDto;

            // Check diagonals
            var firstLeftDiagonalValue = board[0, 0];
            var firstRightDiagonalValue = board[0, 2];
            if (firstLeftDiagonalValue != 0 && firstLeftDiagonalValue == board[1, 1] && firstLeftDiagonalValue == board[2, 2])
            {
                finishedGameDto.Status = GameStatus.Completed;
                finishedGameDto.Winner = (GameIcons)firstLeftDiagonalValue;
            }
            else if (firstRightDiagonalValue != 0 && firstRightDiagonalValue == board[1, 1] && firstRightDiagonalValue == board[2, 0])
            {
                finishedGameDto.Status = GameStatus.Completed;
                finishedGameDto.Winner = (GameIcons)firstRightDiagonalValue;
            }
            else if (boardState.IndexOf('0') == -1) finishedGameDto.Status = GameStatus.Completed;
            
            return finishedGameDto;
        }

        public static bool CheckIfItsPlayersTurn(int playerId, Game game)
        {
            int player1Moves = game.BoardState.Count(c => c == '1');
            int player2Moves = game.BoardState.Count(c => c == '2');

            if (playerId == game.Player1Id)
                return player1Moves == player2Moves;
            else 
                return player2Moves < player1Moves;
        }
    }
}
