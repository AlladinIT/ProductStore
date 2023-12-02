using System.ComponentModel.DataAnnotations;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Configs;


[AttributeUsage(AttributeTargets.Class)]
public class PriceVatInputsAreMathematicallyValidAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var model = (AddProductRequest)value;

        if (model != null)
        {
            if (model.Price != null && model.VatRate != null && model.PriceWithVAT != null)
            {
                decimal expectedPriceWithVat = (decimal)(model.Price * (1 + model.VatRate / 100));
                if (Math.Abs((decimal)model.PriceWithVAT - expectedPriceWithVat) > 0.001m)
                {
                    var errorMessage = $"Mathematically invalid combination of properties: PriceVAT, Price and VatRate";
                    return new ValidationResult(errorMessage);
                }
            
            }
        }

        return ValidationResult.Success;
    }
}