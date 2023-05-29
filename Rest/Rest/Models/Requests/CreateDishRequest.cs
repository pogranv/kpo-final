using System.ComponentModel.DataAnnotations;

namespace Rest.Models.Requests;

/// <summary>
/// Тело создания нового блюда.
/// </summary>
public class CreateDishRequest
{

    /// <summary>
    /// Имя блюда.
    /// </summary>
    [Required(ErrorMessage = "Не указано название")]
    public string Name { get; set; }
    
    /// <summary>
    /// Описание блюда.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Цена блюда.
    /// </summary>
    [Range(0, 1000000, ErrorMessage = "Цена должна лежать в диапазоне [0; 1000000]")]
    [Required(ErrorMessage = "Не указана цена")]
    public decimal Price { get; set; }

    /// <summary>
    /// Количество блюда.
    /// </summary>
    [Required]
    [Range(0, 1000000, ErrorMessage = "Количество должно лежать в диапазоне [0; 1000000]")]
    public int Quantity { get; set; }

    /// <summary>
    /// Доступность блюда.
    /// </summary>
    [Required(ErrorMessage = "Не указана доступность")]
    public bool IsAvailable { get; set; }
}