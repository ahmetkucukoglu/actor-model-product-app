using ProductApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ProductService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/{id}",
    async (int id, ProductService productService) => { return (await productService.GetProduct(id)).Stock; });

app.MapPost("/",
    async (Product product, ProductService productService) => { await productService.Create(product); });

app.MapPost("/decrease/{id}/{stock}",
    async (int id, int stock, ProductService productService) => { await productService.DecreaseStock(id, stock); });

app.MapPost("/increase/{id}/{stock}",
    async (int id, int stock, ProductService productService) => { await productService.IncreaseStock(id, stock); });

app.MapDelete("/{id}",
    async (int id, ProductService productService) => { await productService.Delete(id); });

app.Run();