using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using DR_Tic_Tac_Toe.Common.Errors;

namespace DR_Tic_Tac_Toe.Authentication
{
    public class ValidateUserFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = user.FindFirstValue(ClaimTypes.Name);
            int userId;

            if (string.IsNullOrEmpty(userIdString) || string.IsNullOrEmpty(username))
            {
                context.Result = new UnauthorizedObjectResult(UserErrors.Unauthorized());
                return;
            }
            else if (!int.TryParse(userIdString, out userId))
            {
                context.Result = new UnauthorizedObjectResult(UserErrors.Unauthorized("UserId wrong or corrupted"));
                return;
            }

            context.HttpContext.Items["UserId"] = userId;
            context.HttpContext.Items["Username"] = username;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
