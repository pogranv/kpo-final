using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rest.Helpers;
using Rest.Services.PostgresDbService;
using Rest.Services.PostgresDbService.Models;

namespace Rest.Services;

public class OrdersHandler : IHostedService
{
    private AppSettings _appSettings;

    public OrdersHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
 
    public Task StartAsync(CancellationToken cancellationToken)
    {
        
        ScanOrdersStatuses(cancellationToken);
        return Task.CompletedTask;
    }
 
    private async Task ScanOrdersStatuses(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                UpdateOrderStatuses();
            }
            catch (Exception ex)
            {
                // обработка ошибки однократного неуспешного выполнения фоновой задачи
            }
 
            await Task.Delay(5000, stoppingToken);
        }
    }
    
    private void UpdateOrderStatuses()
    {
        using (RestDbContext db = new RestDbContext())
        {
            var orders = db.Orders.ToList();
            foreach (var order in orders)
            {
                UpdateStatusIfNeed(order);
            }

            db.SaveChanges();
        }
    }
    
    private void UpdateStatusIfNeed(Order order)
    {
        if (order.Status == _appSettings.CompleteOrderStatus || order.Status == _appSettings.CanceledOrderStatus)
        {
            return;
        }
        if ((DateTime.Now - order.UpdatedAt)!.Value.Seconds > _appSettings.UpdateOrderStatusIntervalSeconds)
        {
            order.Status = GetNextStatus(order.Status);
            order.UpdatedAt = DateTime.Now;
        }
    }

    private string GetNextStatus(string currectStatus)
    {
        if (currectStatus == _appSettings.CreatedOrderStatus)
        {
            return _appSettings.ProccessOrderStatus;
        }
        if (currectStatus == _appSettings.ProccessOrderStatus)
        {
            return _appSettings.CompleteOrderStatus;
        }

        return _appSettings.CanceledOrderStatus;
    }
 
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}