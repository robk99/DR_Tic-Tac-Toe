namespace DR_Tic_Tac_Toe.Common.Errors
{
    public static class UserErrors
    {
        public static Error NotFoundByEmail(string username) =>
            BaseErrors.NotFoundByproperty("user", "username", username);

        public static Error NotFoundById(int id) =>
            BaseErrors.NotFoundByproperty("user", "id", id);

        public static Error WrongPassword(string email) =>
            Error.NotFound("The password you provided is not correct.");

        public static Error EmailNotUnique(string username) =>
            Error.Conflict($"User with the username '{username}' already exists.");

        public static Error Unauthorized(string? message = null) =>
            Error.Unauthorized(message ?? "User not authenticated");

        public static Error RegisterFailed() =>
            Error.Problem("Registration failed, try again.");
    }
}
