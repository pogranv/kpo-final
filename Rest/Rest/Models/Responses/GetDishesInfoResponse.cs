using Rest.Services.PostgresDbService.Models;

namespace Rest.Models.Responses;

public class GetDishesInfoResponse
{
    public List<Dish> Dishes { get; set; }
}