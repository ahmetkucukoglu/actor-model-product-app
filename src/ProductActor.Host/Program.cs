using System.Net;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Versions.Compatibility;
using Orleans.Versions.Selector;
using ProductActor.Grains;

var siloPort = int.Parse(args[0]);
var gatewayPort = int.Parse(args[1]);

var silo = new SiloHostBuilder()
    .ConfigureApplicationParts(options =>
    {
        options.AddFromApplicationBaseDirectory();

        options.AddApplicationPart(typeof(ProductStockGrain).Assembly).WithReferences();
    })
    .UseMongoDBClient("mongodb://localhost:2245")
    .AddMongoDBGrainStorage("ProductStockStorage", options =>
    {
        options.DatabaseName = "ProductStockGrains";
    })
    .UseMongoDBClustering(options =>
    {
        options.DatabaseName = "ProductStockCluster";
    })
    .Configure<GrainVersioningOptions>(options =>
    {
        options.DefaultCompatibilityStrategy = nameof(BackwardCompatible);
        options.DefaultVersionSelectorStrategy = nameof(MinimumVersion);
    })
    //.UseLocalhostClustering()
    .ConfigureEndpoints(IPAddress.Loopback, siloPort, gatewayPort)
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "ProductStock";
    })
    .UseDashboard()
    .Build();

await silo.StartAsync();

Console.ReadLine();

await silo.StopAsync();