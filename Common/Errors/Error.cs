namespace DR_Tic_Tac_Toe.Common.Errors
{
    public record Error
    {
        public string Message { get; set; }

        // Add description, status code, etc. if needed

        public Error(string message)
        {
            Message = message;
        }

        public static Error NotFound(string message) =>
                new(message);

        public static Error Conflict(string message) =>
            new(message);

        public static Error Unauthorized(string message) =>
            new(message);

        public static Error Problem(string message) =>
            new(message);

        public static Error BadRequest(string message) =>
            new(message);
    }
}
