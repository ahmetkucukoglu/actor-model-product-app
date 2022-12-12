using Orleans;
using Orleans.CodeGeneration;

namespace ProductActor.Contracts;

[Version(2)]
public interface IProductStock : IGrainWithIntegerKey
{
    Task<int> Get();
    Task Increase(int stock);
    Task Decrease(int stock);
    Task Remove();
}