using Orleans;
using Orleans.Concurrency;
using ProductActor.Contracts;

namespace ProductActor.Grains;

[StatelessWorker]
public class InsufficientStockNotificationGrain : Grain, IInsufficientStockNotification
{
    public Task InsufficientStock(int productId)
    {
        Console.WriteLine($"Stock is insufficient. Product Id: {productId}");

        return Task.CompletedTask;
    }
}