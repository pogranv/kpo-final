using Auth.Models.Requests;
using Auth.Services.PostgresDbService.Models;

namespace Auth.Services.PostgresDbService;

public interface IDbService
{
    public bool IsUserExistByEmail(string email);
    
    public bool IsUserExistByUsername(string username);
    
    public void AddUser(RegistrateUserRequest user);

    public bool IsValidUser(string email, string hashedPassword);

    public Auth.Models.User GetUserById(long id);
    
    public Auth.Models.User GetUserByEmail(string email);

    public void AddSession(long userId, string token);
}

public class PostgresDbService : IDbService
{
    public bool IsUserExistByEmail(string email)
    {
        using (AuthDbContext db = new AuthDbContext())
        {
            return db.Users.FirstOrDefault(user => user.Email == email) != null;
        }
    }

    public bool IsUserExistByUsername(string username)
    {
        using (AuthDbContext db = new AuthDbContext())
        {
            return db.Users.FirstOrDefault(user => user.Username == username) != null;
        }
    }

    public void AddUser(RegistrateUserRequest user)
    {
        var pg_user = new User
        {
            Email = user.Email,
            PasswordHash = user.Password,
            Role = user.Role.ToString().ToLower(),
            Username = user.Username
        };

        using (AuthDbContext db = new AuthDbContext())
        {
            db.Users.Add(pg_user);
            db.SaveChanges();
        }
    }

    public bool IsValidUser(string email, string hashedPassword)
    {
        using (AuthDbContext db = new AuthDbContext())
        {
            return db.Users.FirstOrDefault(user => user.Email == email && user.PasswordHash == hashedPassword) != null;
        }
    }
    

    public Auth.Models.User GetUserById(long id)
    {
        using (AuthDbContext db = new AuthDbContext())
        {
            var pgUser = db.Users.FirstOrDefault(user => user.Id == id);
            return Auth.Models.User.Build(pgUser);
        }
    }

    public Auth.Models.User GetUserByEmail(string email)
    {
        using (AuthDbContext db = new AuthDbContext())
        {
            var pgUser = db.Users.FirstOrDefault(user => user.Email == email);
            return Auth.Models.User.Build(pgUser);
        }
    }

    public void AddSession(long userId, string token)
    {
        using (AuthDbContext db = new AuthDbContext())
        {
            db.Sessions.Add(new Session
            {
                UserId = userId,
                SessionToken = token,
                ExpiresAt = DateTime.Now.AddMinutes(15),
            });
            db.SaveChanges();
        }
    }
}