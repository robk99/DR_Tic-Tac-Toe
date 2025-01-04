using FluentValidation.Results;

namespace DR_Tic_Tac_Toe.Common.Errors
{
    public static class ValidationErrors
    {
        public static Error BadRequest(List<ValidationFailure> errors)
        {
            var combinedMessages = string.Join("; ", errors.Select(e => e.ErrorMessage));

            return Error.BadRequest(combinedMessages);
        }

    }
}
