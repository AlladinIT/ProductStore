namespace WebAPI.Models;

public class Store
{
    public Guid StoreId { get; set; }
    public string StoreName { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}