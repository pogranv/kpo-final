using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Auth.Models;
using Auth.Models.Requests;
using Auth.Services.Exceptions;
using Auth.Services.PostgresDbService;

namespace Auth.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Предоставляет информацию о текущем авторизованном пользователе.
    /// </summary>
    /// <returns>Данные пользователя.</returns>
    [Helpers.Authorize]
    [HttpGet("info")]
    public IActionResult GetInfoAboutUser()
    {
        var infoAboutUser = (User)HttpContext.Items["User"];
        return Ok(infoAboutUser);
    }

    /// <summary>
    /// Осуществляет процедуру авторизации пользователя.
    /// </summary>
    /// <param name="userRequest">Содержимое запроса на авторизацию.</param>
    /// <returns>WT токен для процедуры авторизации.</returns>
    [AllowAnonymous]
    [HttpPost("auth")]
    public IActionResult AuthorizeUser(AuthorizeUserRequest userRequest)
    {
        try
        {
            var jwtToken = _userService.AuthorizeUserAndGetToken(userRequest);
            return Ok(new {jwtToken = jwtToken});
        }
        catch (AuthorizeUserException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Осуществляет процесс регистрации пользователя.
    /// </summary>
    /// <param name="registrateUserRequest">Содержимое запроса на регистрацию.</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegistrateUser(RegistrateUserRequest registrateUserRequest)
    {
        try
        {
            _userService.RegistrateUser(registrateUserRequest);
            return Ok(new {message = "Регистрация пользователя выполнена успешно"});
        }
        catch (UserAlreadyregisterException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}