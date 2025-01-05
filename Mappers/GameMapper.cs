using DR_Tic_Tac_Toe.DTOs.Game;
using DR_Tic_Tac_Toe.DTOs.Game.Requests;
using DR_Tic_Tac_Toe.Enums;
using DR_Tic_Tac_Toe.Models;
using DR_Tic_Tac_Toe.Utils;

namespace DR_Tic_Tac_Toe.Mappers
{
    public class GameMapper
    {
        public GameMapper()
        {
        }

        public GameDetailsDto FromModelToGameDetails(Game game)
        {
            var board = new List<List<string>>();

            for (int row = 0; row < 3; row++)
            {
                board.Add(new List<string> { null, null, null });
            }

            for (int i = 0; i < 9; i++)
            {
                int row = i / 3;
                int col = i % 3;
                char cell = game.BoardState[i];

                if (cell == '1')
                    board[row][col] = GameIcons.X.ToString();
                else if (cell == '2')
                    board[row][col] = GameIcons.O.ToString();
                else
                    board[row][col] = null;
            }

            string nextTurn = game.WinnerId == null && game.Status != (int)GameStatus.Completed
                ? (game.TurnCount % 2 == 0 ? GameIcons.X.ToString() : GameIcons.O.ToString())
                : null;

            var status = game.WinnerId switch
            {
                var winner when game.Player2Id == null => "Waiting for Player 2 to join",
                var winner when winner == game.Player1Id => "Player 1 won",
                var winner when winner == game.Player2Id => "Player 2 won",
                _ when game.Status == (int)GameStatus.Completed => "Draw",
                _ => "Game in progress"
            };

            return new GameDetailsDto
            {
                Board = board,
                NextTurn = nextTurn,
                Status = status
            };
        }

        public Game CreateNewGame(StartNewGameRequest request, int userId)
        {
            var game = new Game
            {
                Player1Id = userId,
                StartTime = DateTime.Now
            };

            var changeBoardDto = Gameutils.SetValueOnABoard(request.Field, game.BoardState, GameIcons.X);
            game.BoardState = changeBoardDto.NewBoardState;

            return game;
        }
    }
}
