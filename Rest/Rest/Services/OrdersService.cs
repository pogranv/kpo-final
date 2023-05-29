using Rest.Models.Requests;
using Rest.Models.Responses;
using Rest.Services.Exceptions;
using Rest.Services.Interfaces;
using Rest.Services.PostgresDbService.Models;

namespace Rest.Services;

public class OrdersService : IOrdersService
{

    private readonly IDbService _db;

    public OrdersService(IDbService db)
    {
        _db = db;
    }

    public long CreateNewOrder(CreateNewOrderRequest request, long userId)
    {
        ValidateOrder(request);
        return _db.AddOrderAndGetId(request, userId);
    }

    public GetOrderResponse GetInfoAboutOrder(long orderId)
    {
        var orderDishes = _db.GetOrderInfo(orderId);
        var orderInfo = orderDishes[0].Order;
        GetOrderResponse response = new GetOrderResponse
        {
            Status = orderInfo.Status,
            SpecialRequests = orderInfo.SpecialRequests,
            CreatedAt = orderInfo.CreatedAt.Value,
            UpdatedAt = orderInfo.UpdatedAt.Value,
            OrderDishes = new List<Dish>()
        };
        foreach (var orderDish in orderDishes)
        {
            response.OrderDishes.Add(orderDish.Dish);
        }

        return response;
    }

    private void ValidateOrder(CreateNewOrderRequest request)
    {
        var availableDishes = _db.GetAvailableDishes();
        foreach (var orderDish in request.Dishes)
        {
            var availableDish = availableDishes.FirstOrDefault(dish => dish.Id == orderDish.DishId);
            if (availableDish == null)
            {
                throw new IncorrectOrderException($"Блюдо с id={orderDish.DishId} недоступно в меню");
            }

            if (availableDish.Quantity < orderDish.Quantity)
            {
                throw new IncorrectOrderException($"Количества блюда id={orderDish.DishId} не хватает в наличии для заказа");
            }

            availableDish.Quantity -= orderDish.Quantity;
        }
    }
}