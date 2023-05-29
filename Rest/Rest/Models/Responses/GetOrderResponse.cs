using Rest.Services.PostgresDbService.Models;

namespace Rest.Models.Responses;

public class GetOrderResponse
{
    
    public string Status { get; set; }

    public string? SpecialRequests { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<Dish> OrderDishes { get; set; }
}