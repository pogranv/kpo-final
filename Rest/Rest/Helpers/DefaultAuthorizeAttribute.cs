using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rest.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DefaultAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        int? userId = (int?)context.HttpContext.Items["UserId"];
        if (userId == null)
        {
            context.Result = new JsonResult(new { message = "Пользователь не авторизирован" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}

