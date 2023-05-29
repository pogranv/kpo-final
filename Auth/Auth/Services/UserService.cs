using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Auth.Helpers;
using Auth.Models;
using Auth.Models.Requests;
using Auth.Services.Exceptions;

namespace Auth.Services.PostgresDbService;

public class UserService : IUserService
{
    private readonly ApplicationSettings _applicationSettings;
    private readonly IDbService _db;

    public UserService(IOptions<ApplicationSettings> appSettings, IDbService db)
    {
        _applicationSettings = appSettings.Value;
        _db = db;
    }

    public void RegistrateUser(RegistrateUserRequest request)
    {
        if (_db.IsUserExistByEmail(request.Email))
        {
            throw new UserAlreadyregisterException("Пользователь с такой почтой уже зарегистрирован!");
        }
        if (_db.IsUserExistByUsername(request.Username))
        {
            throw new UserAlreadyregisterException("Пользователь с таким ником уже зарегистрирован!");
        }

        request.Password = Hasher.HashPassword(request.Password);
        _db.AddUser(request);
    }

    public string AuthorizeUserAndGetToken(AuthorizeUserRequest userRequest)
    {
        if (!_db.IsValidUser(userRequest.Email, Hasher.HashPassword(userRequest.Password)))
        {
            throw new AuthorizeUserException("`Неправильная почта или пароль.");
        }

        var user = _db.GetUserByEmail(userRequest.Email);
        var token = GenerateJwtToken(user);
        _db.AddSession(user.Id, token);
        
        return token;
    }
    
    public User GetUserById(long id)
    {
        return _db.GetUserById(id);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_applicationSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}