using Microsoft.AspNetCore.Mvc;

using Rest.Helpers;
using Rest.Models.Requests;
using Rest.Services.Exceptions;
using Rest.Services.Interfaces;

namespace Rest.Controllers;

[ApiController]
[Route("api/v1/dishes")]
public class DishesController : ControllerBase
{
    private IDishesService _dishesService;

    public DishesController(IDishesService dishesService)
    {
        _dishesService = dishesService;
    }

    /// <summary>
    /// Предоставляет информацию о блюдах, доступных в меню.
    /// </summary>
    /// <returns>Данные о блюдах.</returns>
    [DefaultAuthorize]
    [HttpGet("available")]
    public IActionResult GetInfoAboutAvailableDishes()
    {
        var resp = _dishesService.GetInfoAboutAvailableDishes();
        return Ok(resp);
    }
    
    /// <summary>
    /// Позволяет изменять информацию о блюде частично или полностью по его идентификатору.
    /// </summary>
    /// <param name="dishId">Идентификатор блюда.</param>
    /// <param name="request">Тело запроса, которое содержит информацию о том, какие поля нужно изменить и какими значениями их заменить.</param>
    /// <returns></returns>
    [ManagerAuthorize]
    [HttpPatch("change/{dishId:long:min(0)}")]
    public IActionResult ChangeInfoAboutDish(long dishId,
        [FromBody] ChangeInfoAboutDishRequest request)
    {
        try
        {
            _dishesService.ChangeInfoAboutDish(dishId, request);
            return Ok();
        }
        catch (DishNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Осуществляет удаление блюда из меню.
    /// </summary>
    /// <param name="dishId">Идентификатор удаляемого блюда.</param>
    /// <returns></returns>
    [ManagerAuthorize]
    [HttpDelete("remove/{dishId:long:min(0)}")]
    public IActionResult DeleteDish(long dishId)
    {
        try
        {
            _dishesService.DeleteDish(dishId);
            return Ok();
        }
        catch (DishNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Осуществляет добавление нового блюда в меню.
    /// </summary>
    /// <param name="request">Содержимое запроса.</param>
    /// <returns>Идентификатор только что созданного блюда.</returns>
    [ManagerAuthorize]
    [HttpPost("create")]
    public IActionResult CreateNewDish(CreateDishRequest request)
    {
        var resp = _dishesService.CreateNewDish(request);
        return Ok(resp);
    }

    /// <summary>
    /// Предоставляет информацию о запрошенных блюдах.
    /// </summary>
    /// <param name="request">Идентификаторы запрашиваемых блюд. Если оставить список пустым, будут возвращены данные по всем блюдам.</param>
    /// <returns>Информация о запрашиваемых блюдах.</returns>
    [ManagerAuthorize]
    [HttpPost]
    public IActionResult GetInfoAboutRequiredDishes(GetDishesInfoRequest request)
    {
        var resp = _dishesService.GetInfoAboutRequiredDishes(request);
        return Ok(resp);
    }
}