namespace DR_Tic_Tac_Toe.Common.Errors
{
    public static class GameErrors
    {
        public static Error NotFoundById(int id) =>
            Error.NotFound($"The game with the id '{id}' was not found.");

        public static Error YouAreAlreadyPlaying(int id) =>
            Error.Conflict($"Cannot join the game with the id '{id}' " +
               $"because you are already part of this game.");

        public static Error MaxNumberOfPlayers(int id) =>
            Error.Conflict($"Cannot join the game with the id '{id}' " +
             $"because maximum number of players are already playing this game");

        public static Error GameFinished(int id) =>
            Error.Conflict($"Cannot join the game with the id '{id}' " +
            $"because this game is already finished.");
    }
}
