namespace ProductActor.Tests.Api;

public class ApiTests : IClassFixture<ApiTestsFixture>
{
    private readonly ApiTestsFixture _fixture;

    public ApiTests(ApiTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Test()
    {
        var task1 = Task.Run(async () => { await _fixture.HttpClient.PostAsync($"/increase/{_fixture.ProductId}/{5}", null); });
        var task2 = Task.Run(async () => { await _fixture.HttpClient.PostAsync($"/decrease/{_fixture.ProductId}/{2}", null); });
        var task3 = Task.Run(async () => { await _fixture.HttpClient.PostAsync($"/decrease/{_fixture.ProductId}/{2}", null); });

        await Task.WhenAll(task1, task2, task3);

        var responseMessage = await _fixture.HttpClient.GetAsync($"/{_fixture.ProductId}");
        var response = await responseMessage.Content.ReadAsStringAsync();

        Assert.Equal(1, int.Parse(response));
    }
}