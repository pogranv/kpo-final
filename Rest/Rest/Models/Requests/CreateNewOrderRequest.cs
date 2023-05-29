using System.ComponentModel.DataAnnotations;

namespace Rest.Models.Requests;

/// <summary>
/// Тело создания заказа.
/// </summary>
public class CreateNewOrderRequest
{
    /// <summary>
    /// Список блюд.
    /// </summary>
    [Required]
    public List<DishDetails> Dishes { get; set; }
    
    /// <summary>
    /// Пожелания по заказу.
    /// </summary>
    public string? SpecialRequests { get; set; }
}