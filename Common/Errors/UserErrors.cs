namespace DR_Tic_Tac_Toe.Common.Errors
{
    public static class UserErrors
    {
        public static Error NotFoundByEmail(string username) =>
            Error.NotFound($"The user with the username '{username}' was not found.");

        public static Error WrongPassword(string email) =>
            Error.NotFound("The password you provided is not correct.");

        public static Error EmailNotUnique(string username) =>
            Error.Conflict($"User with the username '{username}' already exists.");

        public static Error Unauthorized(string? message = null) =>
            Error.Unauthorized(message ?? "User not authenticated");
    }
}
