using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rest.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ManagerAuthorizeAttribute : Attribute, IAuthorizationFilter
{

    private readonly string _managerRole = "manager";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        int? userId = (int?)context.HttpContext.Items["UserId"];
        if (userId == null)
        {
            context.Result = new JsonResult(new { message = "Пользователь не авторизирован" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        string? userRole = (string?)context.HttpContext.Items["Role"];
        if (userRole == null || userRole != _managerRole)
        {
            context.Result = new JsonResult(new { message = "Пользователь не является менеджером" }) { StatusCode = StatusCodes.Status403Forbidden };
        }
    }
}