namespace Rest.Services.PostgresDbService.Models;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Status { get; set; } = null!;

    public string? SpecialRequests { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<OrderDish> OrderDishes { get; set; } = new List<OrderDish>();

    public virtual User User { get; set; } = null!;

    public override string ToString()
    {
        return $"{Id}, {UserId}, {Status}, {CreatedAt} {UpdatedAt}";
    }
}
