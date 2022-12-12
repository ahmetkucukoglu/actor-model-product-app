using Orleans;
using Orleans.Configuration;
using ProductActor.Contracts;

var builder = WebApplication.CreateBuilder(args);

var clientBuilder = new ClientBuilder()
    .UseMongoDBClient("mongodb://localhost:2245")
    .UseMongoDBClustering(options =>
    {
        options.DatabaseName = "ProductStockCluster";
    })
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "ProductStock";
    });
    //.UseLocalhostClustering(30000, "ProductStock", "dev");

var client = clientBuilder.Build();

await client.Connect();

builder.Services.AddSingleton(client);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/{id}",
    async (int id, IClusterClient clusterClient) =>
    {
        var productStock = clusterClient.GetGrain<IProductStock>(id);

        return await productStock.Get();
    });

app.MapPost("/decrease/{id}",
    async (int id, int stock, IClusterClient clusterClient) =>
    {
        var productStock = clusterClient.GetGrain<IProductStock>(id);

        await productStock.Decrease(stock);
    });

app.MapPost("/increase/{id}",
    async (int id, int stock, IClusterClient clusterClient) =>
    {
        var productStock = clusterClient.GetGrain<IProductStock>(id);

        await productStock.Increase(stock);
    });

app.MapPost("/remove/{id}",
    async (int id, IClusterClient clusterClient) =>
    {
        var productStock = clusterClient.GetGrain<IProductStock>(id);

        await productStock.Remove();
    });

app.Run();