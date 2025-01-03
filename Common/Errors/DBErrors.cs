namespace DR_Tic_Tac_Toe.Common.Errors
{
    public class DBErrors
    {
        private static Error CommandFailed(string command) =>
            Error.Problem($"Database {command} failed. Please try again later.");

        public static Error UpdateFailed() =>
            CommandFailed("UPDATE");
        public static Error CreateFailed() =>
            CommandFailed("CREATE");
    }
}
