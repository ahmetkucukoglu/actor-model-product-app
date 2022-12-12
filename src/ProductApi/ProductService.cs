using MongoDB.Driver;

namespace ProductApi;

public class ProductService
{
    private readonly IMongoCollection<Product> _collection;

    public ProductService()
    {
        var client = new MongoClient("mongodb://localhost:2245");
        var database = client.GetDatabase("e-commerce");

        _collection = database.GetCollection<Product>("products");
    }

    public async Task Create(Product product)
    {
        await _collection.InsertOneAsync(product);
    }

    public async Task<Product> GetProduct(int id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var products = await _collection.FindAsync(filter);

        return await products.FirstOrDefaultAsync();
    }

    public async Task DecreaseStock(int id, int stock)
    {
        var product = await GetProduct(id);
        product.DecreaseStock(stock);

        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var update = Builders<Product>.Update.Set(p => p.Stock, product.Stock);

        await _collection.UpdateOneAsync(filter, update);

        Console.WriteLine($"Decrease: {stock}, Now: {product.Stock}");
    }

    public async Task IncreaseStock(int id, int stock)
    {
        var product = await GetProduct(id);
        product.IncreaseStock(stock);

        Console.WriteLine($"Increase: {stock}, Now: {product.Stock}");

        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var update = Builders<Product>.Update.Set(p => p.Stock, product.Stock);

        await _collection.UpdateOneAsync(filter, update);
    }

    public async Task Delete(int id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        await _collection.DeleteOneAsync(filter);
    }
}