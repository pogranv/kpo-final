using Rest.Models.Requests;
using Rest.Models.Responses;

namespace Rest.Services.Interfaces;

public interface IDishesService
{
    public CreateDishResponse CreateNewDish(CreateDishRequest request);
    public GetDishesInfoResponse GetInfoAboutRequiredDishes(GetDishesInfoRequest request);

    public void DeleteDish(long dishId);

    public void ChangeInfoAboutDish(long dishId, ChangeInfoAboutDishRequest request);
    
    public GetDishesInfoResponse GetInfoAboutAvailableDishes();
}