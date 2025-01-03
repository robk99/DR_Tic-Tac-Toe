namespace DR_Tic_Tac_Toe.Common.Errors
{
    public record Error
    {
        public string Message { get; set; }
        public string Description { get; }

        public Error(string message, string? description = null)
        {
            Message = message;
            Description = description ?? string.Empty;
        }

        public static Error NotFound(string message) =>
                new(message);

        public static Error Conflict(string message) =>
            new(message);

        public static Error Unauthorized(string message) =>
            new(message);
    }
}
