using System.ComponentModel.DataAnnotations;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Configs;

[AttributeUsage(AttributeTargets.Class)]
public class AtLeastTwoPropertiesRequiredAttribute : ValidationAttribute
{
    private readonly string[] _propertyNames;

    public AtLeastTwoPropertiesRequiredAttribute(params string[] propertyNames)
    {
        _propertyNames = propertyNames;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var model = (AddProductRequest)value;
        int providedPropertyCount = 0;


        if (model != null)
        {
            foreach (var property in _propertyNames)
            {
                if (model.GetType().GetProperty(property) != null)
                {
                    if (model.GetType().GetProperty(property)!.GetValue(model) != null)
                    {
                        providedPropertyCount++;
                    }
                }
                else
                {
                    var errorMessage = $"Non-existent property: {property}";
                    return new ValidationResult(errorMessage, _propertyNames);
                }
            }
        
            if (providedPropertyCount < 2)
            {
                var errorMessage = $"At least 2 out of {string.Join(", ", _propertyNames)} are required.";
                return new ValidationResult(errorMessage, _propertyNames);
            }

            
        }

        return ValidationResult.Success;
    }
}