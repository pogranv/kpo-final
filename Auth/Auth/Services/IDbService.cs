using Auth.Models;
using Auth.Models.Requests;

namespace Auth.Services.PostgresDbService;

public interface IUserService
{
    void RegistrateUser(RegistrateUserRequest request);

    public string AuthorizeUserAndGetToken(AuthorizeUserRequest userRequest);
    
    User GetUserById(long id);
}