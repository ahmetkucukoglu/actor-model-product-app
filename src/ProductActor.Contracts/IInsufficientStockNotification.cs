using Orleans;

namespace ProductActor.Contracts;

public interface IInsufficientStockNotification : IGrainWithIntegerKey
{
    Task InsufficientStock(int productId);
}