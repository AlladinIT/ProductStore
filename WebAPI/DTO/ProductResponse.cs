namespace WebAPI.DTO;

public class ProductResponse
{
    public String ProductName { get; set; }
    public String GroupName { get; set; }
    public DateTime Time { get; set; }
    public decimal Price { get; set; }
    public decimal PriceWithVAT { get; set; }
    public decimal VatRate { get; set; }
    
    public List<String>? StoresNames { get; set; }
}