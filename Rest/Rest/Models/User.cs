using System.Text.Json.Serialization;

namespace Rest.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Role { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    // public static User Build(Rest.Services.User user)
    // {
    //     return new User
    //     {
    //         Id = user.Id,
    //         Username = user.Username,
    //         Email = user.Email,
    //         Role = user.Role
    //     };
    // }
}