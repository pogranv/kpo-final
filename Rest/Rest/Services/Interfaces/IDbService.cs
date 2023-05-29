using Rest.Models.Requests;
using Rest.Services.PostgresDbService.Models;

namespace Rest.Services.Interfaces;

public interface IDbService
{
    public long CreateDishAndGetId(Dish dish);
    public List<Dish> GetAllDishes();
    public List<Dish> GetDishesInfoByIds(List<long> ids);
    
    public List<Dish> GetAvailableDishes();

    public void RemoveDish(long dishId);

    public void ChangeDish(long dishId, ChangeInfoAboutDishRequest request);

    public long AddOrderAndGetId(CreateNewOrderRequest request, long userId);

    public List<OrderDish> GetOrderInfo(long orderId);
}