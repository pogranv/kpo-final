using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rest.Helpers;
using Rest.Models.Requests;
using Rest.Services.Exceptions;
using Rest.Services.Interfaces;
using Rest.Services.PostgresDbService.Models;

namespace Rest.Services.PostgresDbService;

public class PostgresDbService : IDbService
{
    private AppSettings _appSettings;

    public PostgresDbService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public List<OrderDish> GetOrderInfo(long orderId)
    {
        List<Dish> dishes = new List<Dish>();
        using (RestDbContext db = new RestDbContext())
        {
            var orderDishes = db.OrderDishes.Where(orderDish => orderDish.OrderId == orderId).Include(order => order.Dish).Include(order => order.Order).ToList();
            if (orderDishes == null)
            {
                throw new OrderNotFoundException("Заказ не найден");
            }
            return orderDishes;
        }
    }
    public long CreateDishAndGetId(Dish dish)
    {
        using (RestDbContext db = new RestDbContext())
        {
            db.Dishes.Add(dish);
            db.SaveChanges();
            return dish.Id;
        }
    }

    public List<Dish> GetAllDishes()
    {
        using (RestDbContext db = new RestDbContext())
        {
            return db.Dishes.ToList();
        }
    }

    public List<Dish> GetDishesInfoByIds(List<long> ids)
    {
        using (RestDbContext db = new RestDbContext())
        {
            return db.Dishes.Where(dish => ids.Contains(dish.Id)).ToList();
        }
    }

    public List<Dish> GetAvailableDishes()
    {
        using (RestDbContext db = new RestDbContext())
        {
            return db.Dishes.Where(dish => dish.IsAvailable && dish.Quantity > 0).ToList();
        }
    }

    public void RemoveDish(long dishId)
    {
        using (RestDbContext db = new RestDbContext())
        {
            var dish = db.Dishes.FirstOrDefault(dish => dish.Id == dishId);
            if (dish == null)
            {
                throw new DishNotFoundException($"Блюдо с id={dishId} не найдено");
            }
            db.Dishes.Remove(dish);
            db.SaveChanges();
        }
    }

    public void ChangeDish(long dishId, ChangeInfoAboutDishRequest request)
    {
        using (RestDbContext db = new RestDbContext())
        {
            var dish = db.Dishes.FirstOrDefault(dish => dish.Id == dishId);
            if (dish == null)
            {
                throw new DishNotFoundException();
            }

            dish.Name = request.Name ?? dish.Name;
            dish.Description = request.Description ?? dish.Description;
            dish.Price = request.Price ?? dish.Price;
            dish.Quantity = request.Quantity ?? dish.Quantity;
            dish.IsAvailable = request.IsAvailable ?? dish.IsAvailable;
            db.SaveChanges();
        }
    }

    public long AddOrderAndGetId(CreateNewOrderRequest request, long userId)
    {
        using (RestDbContext db = new RestDbContext())
        {
            var order = new Order
            {
                SpecialRequests = request.SpecialRequests,
                Status = _appSettings.CreatedOrderStatus,
                UserId = userId,
            };
            db.Orders.Add(order);
            db.SaveChanges();
            
            foreach (var dish in request.Dishes)
            {
                var dbDish = db.Dishes.FirstOrDefault(d => d.Id == dish.DishId);
                var orderDish = new OrderDish
                {
                    OrderId = order.Id,
                    DishId = dish.DishId,
                    Quantity = dish.Quantity,
                    Price = dbDish.Price,
                };
                dbDish.Quantity -= dish.Quantity;
                db.OrderDishes.Add(orderDish);
            }
            db.SaveChanges();
            return order.Id;
        }
    }
    
    
}