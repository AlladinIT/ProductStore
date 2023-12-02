namespace WebAPI.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public String ProductName { get; set; }
    public DateTime TimeAdded { get; set; }
    public decimal Price { get; set; }
    public decimal PriceWithVAT { get; set; }
    public decimal VatRate { get; set; }
    
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    
    public List<Store> Stores { get; set; } = new List<Store>();
}