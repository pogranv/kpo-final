namespace Auth.Models.Requests;

using System.ComponentModel.DataAnnotations;

public class RegistrateUserRequest
{
    /// <summary>
    /// Почта пользователя.
    /// </summary>
    [Required(ErrorMessage = "Не указана почта")]
    [StringLength(100, ErrorMessage = "Максимальная длина 100 символов")]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес электронной почты")]
    public string Email { get; set; }
    
    /// <summary>
    /// Ник пользователя.
    /// </summary>
    [Required(ErrorMessage = "Не указан ник")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Максимальная длина - 50 символов, минимальная - 3")]
    public string Username { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Максимальная длина - 50 символов, минимальная - 3")]
    public string Password { get; set; }
    
    /// <summary>
    /// Роль пользователя:
    /// 0 - customer
    /// 1 - chef
    /// 2 - manager
    /// </summary>
    [Required(ErrorMessage = "Не указана роль")]
    [Range(0, 2, ErrorMessage = "Недопустимая роль. Допустимые: 0 - Customer, 1 - Chef, 2 - Manager")]
    public Role Role { get; set; }
}