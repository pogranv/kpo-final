namespace Rest.Helpers;

public class AppSettings
{
    public string Secret { get; set; }
    
    public string CreatedOrderStatus { get; } = "created";
    public string ProccessOrderStatus { get; } = "proccess";
    public string CompleteOrderStatus { get; } = "complete";
    public string CanceledOrderStatus { get; } = "canceled";

    public int UpdateOrderStatusIntervalSeconds { get; } = 10;
}