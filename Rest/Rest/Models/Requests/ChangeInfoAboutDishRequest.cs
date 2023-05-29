namespace Rest.Models.Requests;

/// <summary>
/// Тело изменения блюда.
/// </summary>
public class ChangeInfoAboutDishRequest
{
    /// <summary>
    /// Новое название блюда (опционально).
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Новое описание блюда (опционально).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Новая цена блюда (опционально).
    /// </summary>
    public decimal? Price { get; set; }
    
    /// <summary>
    /// Новое количество блюда (опционально).
    /// </summary>
    public int? Quantity { get; set; }
    
    /// <summary>
    /// Доступность блюда (опционально).
    /// </summary>
    public bool? IsAvailable { get; set; }
}