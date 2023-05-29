using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Auth.Helpers;
using Auth.Services.PostgresDbService;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ApplicationSettings _applicationSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<ApplicationSettings> appSettings)
    {
        _next = next;
        _applicationSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            AttachUserToContext(context, userService, token);
        }
        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_applicationSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            
            context.Items["User"] = userService.GetUserById(userId);
        }
        catch
        {
            // ignored
        }
    }
}