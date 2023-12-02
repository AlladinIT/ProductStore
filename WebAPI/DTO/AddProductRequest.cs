using System.ComponentModel.DataAnnotations;
using WebAPI.Configs;

namespace WebAPI.DTO;

[AtLeastTwoPropertiesRequired("Price", "PriceWithVAT", "VatRate")]
[PriceVatInputsAreMathematicallyValid]
public class AddProductRequest
{
    [Length(1,50)]
    public String ProductName { get; set; }
    [Range(0.1, 99999)]
    public decimal? Price { get; set; }
    [Range(0, 99999)]
    public decimal? PriceWithVAT { get; set; }
    [Range(0, 100)]
    public decimal? VatRate { get; set; }
    
    [Required]
    public Guid? GroupId { get; set; }
    
    public List<Guid>? StoresIds { get; set; }
    
        
    public void CalculateMissingValues()
    {
        if (Price == null && PriceWithVAT != null && VatRate != null)
        {
            Price = PriceWithVAT / (1 + VatRate / 100);
        }
        else if (Price != null && PriceWithVAT == null && VatRate != null)
        {
            PriceWithVAT = Price * (1 + (VatRate / 100));
        }
        else if (Price != null && PriceWithVAT != null && VatRate == null)
        {
            VatRate = (PriceWithVAT / Price - 1) * 100;
        }
    }
}