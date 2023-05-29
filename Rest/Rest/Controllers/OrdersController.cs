using Microsoft.AspNetCore.Mvc;

using Rest.Helpers;
using Rest.Models.Requests;
using Rest.Services.Exceptions;
using Rest.Services.Interfaces;

namespace Rest.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    /// <summary>
    /// Предоставляет информацию о заказе.
    /// </summary>
    /// <param name="orderId">Id заказа</param>
    /// <returns>Данные о заказе.</returns>
    [DefaultAuthorize]
    [HttpGet("info/{orderId:long:min(0)}")]
    public IActionResult GetInfoAboutOrder(long orderId)
    {
        try
        {
            var resp = _ordersService.GetInfoAboutOrder(orderId);
            return Ok(resp);
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Осуществляет создание нового заказа.
    /// </summary>
    /// <param name="request">Информация о заказе.</param>
    /// <returns>Идентификатор только что созданного заказа.</returns>
    [DefaultAuthorize]
    [HttpPost("create")]
    public IActionResult CreateNewOrder(CreateNewOrderRequest request)
    {
        try
        {
            var userId = (int)HttpContext.Items["UserId"];
            var orderId = _ordersService.CreateNewOrder(request, userId);
            return Ok(new { message = "Заказ успешно создан.", orderId = orderId });
        }
        catch (IncorrectOrderException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}