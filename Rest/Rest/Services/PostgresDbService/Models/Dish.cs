using System.Text.Json.Serialization;

namespace Rest.Services.PostgresDbService.Models;

public partial class Dish
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool IsAvailable { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<OrderDish> OrderDishes { get; set; } = new List<OrderDish>();
}
