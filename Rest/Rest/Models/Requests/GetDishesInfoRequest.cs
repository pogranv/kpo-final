using System.ComponentModel.DataAnnotations;

namespace Rest.Models.Requests;

/// <summary>
/// Тело запроса получения блюд.
/// </summary>
public class GetDishesInfoRequest
{
    /// <summary>
    /// Список id блюд, по которым нужно вернуть информаицю. Если список
    /// пустой, возвращаются все существующие блюда. Возвращаюстся даже
    /// недоступные блюда.
    /// </summary>
    [Required(ErrorMessage = "Укажите список айдишников запрашиваемых блюд, можно отправить пустым")]
    public List<long> RequiredIds { get; set; }
}