using System.Net.Http.Json;
using ProductApi;

namespace ProductActor.Tests.Api;

public class ApiTestsFixture : IDisposable
{
    private readonly ApiApplication _application;
    public readonly HttpClient HttpClient;

    public int ProductId = 1;

    public ApiTestsFixture()
    {
        _application = new ApiApplication();

        HttpClient = _application.CreateClient();

        CreateProduct();
    }

    public void Dispose()
    {
        DeleteDatabase();

        HttpClient.Dispose();

        _application.Dispose();
    }

    private void CreateProduct()
    {
        HttpClient.PostAsJsonAsync("/", new Product {Id = ProductId, Stock = 0}).Wait();
    }

    private void DeleteDatabase()
    {
        HttpClient.DeleteAsync($"/{ProductId}").Wait();
    }
}