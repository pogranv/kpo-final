

using Rest.Models.Requests;
using Rest.Models.Responses;
using Rest.Services.Interfaces;
using Rest.Services.PostgresDbService.Models;

namespace Rest.Services;

public class DishesService : IDishesService
{

    // private readonly AppSettings _appSettings;
    private readonly IDbService _db;

    public DishesService(IDbService db)
    {
        // _appSettings = appSettings.Value;
        _db = db;
    }

    public CreateDishResponse CreateNewDish(CreateDishRequest request)
    {
        Dish dish = new Dish
        {
            Name = request.Name,
            Price = request.Price,
            Description = request.Description,
            Quantity = request.Quantity,
            IsAvailable = request.IsAvailable
        };
        var dishId = _db.CreateDishAndGetId(dish);
        return new CreateDishResponse { CreatedDishId = dishId };
    }

    public GetDishesInfoResponse GetInfoAboutRequiredDishes(GetDishesInfoRequest request)
    {
        List<Dish> dishes;
        if (request.RequiredIds.Count == 0)
        {
            dishes = _db.GetAllDishes();
        }
        else
        {
            dishes = _db.GetDishesInfoByIds(request.RequiredIds);
        }
        return new GetDishesInfoResponse
        {
            Dishes = dishes
        };
    }

    public GetDishesInfoResponse GetInfoAboutAvailableDishes()
    {
        var dishes = _db.GetAvailableDishes();
        return new GetDishesInfoResponse { Dishes = dishes };
    }

    public void DeleteDish(long dishId)
    {
        _db.RemoveDish(dishId);
    }

    public void ChangeInfoAboutDish(long dishId, ChangeInfoAboutDishRequest request)
    {
        _db.ChangeDish(dishId, request);
    }
}