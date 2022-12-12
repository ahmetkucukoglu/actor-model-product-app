using Orleans;
using Orleans.TestingHost;
using ProductActor.Contracts;

namespace ProductActor.Tests;

public class ActorTests
{
    public IGrainFactory GrainFactory { get; }
    
    public ActorTests()
    {
        var builder = new TestClusterBuilder();

        var cluster = builder.Build();
        cluster.Deploy();

        GrainFactory = cluster.GrainFactory;
    }
    
    [Fact]
    public async Task Test()
    {
        var productStock = GrainFactory.GetGrain<IProductStock>(1);
        
        var task1 = Task.Run(async () => { await productStock.Increase(5); });
        var task2 = Task.Run(async () => { await productStock.Decrease(2); });
        var task3 = Task.Run(async () => { await productStock.Decrease(2); });

        await Task.WhenAll(task1, task2, task3);

        var stock = await productStock.Get();
        
        Assert.Equal(1, stock);
    }
}