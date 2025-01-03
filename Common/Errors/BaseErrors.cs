namespace DR_Tic_Tac_Toe.Common.Errors
{
    public static class BaseErrors
    {
        public static Error NotFoundByproperty(string objectName, string propertyName, string propertyValue) =>
            Error.NotFound($"The {objectName} with the {propertyName}: '{propertyValue}' was not found.");

        public static Error NotFoundByproperty(string objectName, string propertyName, int propertyValue) =>
            Error.NotFound($"The {objectName} with the {propertyName}: '{propertyValue}' was not found.");
    }
}
