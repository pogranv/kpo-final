using System.ComponentModel.DataAnnotations;

namespace Rest.Models.Requests;

/// <summary>
/// Детали заказа блюда.
/// </summary>
public class DishDetails
{
    /// <summary>
    /// Id блюда.
    /// </summary>
    [Required]
    public long DishId { get; set; }
    
    /// <summary>
    /// Количество блюда.
    /// </summary>
    [Required]
    [Range(1, 100, ErrorMessage = "Количество одного блюда не должно превышать 100 и быть меньше 1")]
    public int Quantity { get; set; }
}