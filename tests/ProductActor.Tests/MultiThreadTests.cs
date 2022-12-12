namespace ProductActor.Tests;

public class MultiThreadTests
{
    [Fact]
    public async void Test()
    {
        var increment = new Increment();

        var task1 = Task.Run(() => { increment.Increase(5); });
        var task2 = Task.Run(() => { increment.Decrease(2); });
        var task3 = Task.Run(() => { increment.Decrease(2); });

        await Task.WhenAll(task1, task2, task3);

        Assert.Equal(1, increment.Count);
    }
}