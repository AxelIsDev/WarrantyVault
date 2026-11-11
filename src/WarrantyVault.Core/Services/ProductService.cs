using WarrantyVault.Core.Enums;
using WarrantyVault.Core.Models;
using WarrantyVault.Core.Validation;

namespace WarrantyVault.Core.Services;

public class ProductService
{   private readonly List<Product> _products = new();
    
    public IReadOnlyList<Product>GetAll()
    {
        return _products;
    }

    public List<ProductValidationError> Add(Product product)
    {   
        ArgumentNullException.ThrowIfNull(product);
        
        var errors = ProductValidator.Validate(product);

        if (!errors.Any())
        {
            _products.Add(product);
        }
        
        return errors;
    }
}