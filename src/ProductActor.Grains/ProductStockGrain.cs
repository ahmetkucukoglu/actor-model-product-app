using Orleans;
using Orleans.Providers;
using ProductActor.Contracts;

namespace ProductActor.Grains;

[StorageProvider(ProviderName = "ProductStockStorage")]
public class ProductStockGrain : Grain<ProductStockState>, IProductStock
{
    public override Task OnActivateAsync()
    {
        Console.WriteLine($"ProductStock is activated. Product Id : {this.GetPrimaryKeyLong()}");
        
        return Task.CompletedTask;
    }

    public override Task OnDeactivateAsync()
    {
        Console.WriteLine($"ProductStock is deactivated. Product Id : {this.GetPrimaryKeyLong()}");
        
        return Task.CompletedTask;
    }

    public Task<int> Get()
    {
        return Task.FromResult(State.Stock);
    }

    public async Task Increase(int stock)
    {
        State.Stock += stock;
        
        Console.WriteLine($"Stock is increased. Product Id : {this.GetPrimaryKeyLong()}");

        await WriteStateAsync();
    }

    public async Task Decrease(int stock)
    {
        if (State.Stock < stock)
        {
            var notification = GrainFactory.GetGrain<IInsufficientStockNotification>(0);
            await notification.InsufficientStock((int)this.GetPrimaryKeyLong());
            
            return;
        }
        
        State.Stock -= stock;
        
        Console.WriteLine($"Stock is decreased. Product Id : {this.GetPrimaryKeyLong()}");
        
        await WriteStateAsync();
    }

    public Task Remove()
    {
        Console.WriteLine($"Stock is removed. Product Id : {this.GetPrimaryKeyLong()}");
        
        return Task.CompletedTask;
    }
}