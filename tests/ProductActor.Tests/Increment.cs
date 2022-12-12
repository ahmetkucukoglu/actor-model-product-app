namespace ProductActor.Tests;

public class Increment
{
    private readonly object _lock = new();

    public int Count { get; private set; }

    public void Increase(int count)
    {
        lock (_lock)
        {
            Count += count;
        }
    }

    public void Decrease(int count)
    {
        lock (_lock)
        {
            Count -= count;
        }
    }
}