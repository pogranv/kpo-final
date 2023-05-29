namespace Rest.Services.PostgresDbService.Models;

public partial class OrderDish
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long DishId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
