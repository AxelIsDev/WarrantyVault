using WarrantyVault.Core.Enums;
using WarrantyVault.Core.Models;

namespace WarrantyVault.Core.Validation;

public static class ProductValidator
{
    public static List<ProductValidationError> Validate(Product product)
    {
        var errors = new List<ProductValidationError>();

        if (string.IsNullOrWhiteSpace(product.Name))
        {
            errors.Add(ProductValidationError.NameRequired);
        }
        
        if (string.IsNullOrWhiteSpace(product.Brand))
        {
            errors.Add(ProductValidationError.BrandRequired);
        }
        
        if (string.IsNullOrWhiteSpace(product.Model))
        {
            errors.Add(ProductValidationError.ModelRequired);
        }

        return errors;
    }
}