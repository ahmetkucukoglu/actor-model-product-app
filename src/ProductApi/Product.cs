namespace ProductApi;

public class Product
{
    public int Id { get; set; }
    public int Stock { get; set; }
    
    public void IncreaseStock(int stock)
    {
        Stock += stock;
    }

    public void DecreaseStock(int stock)
    {
        Stock -= stock;
    }
}