using Rest.Models.Requests;
using Rest.Models.Responses;

namespace Rest.Services.Interfaces;

public interface IOrdersService
{
    public long CreateNewOrder(CreateNewOrderRequest request, long userId);

    public GetOrderResponse GetInfoAboutOrder(long orderId);
}