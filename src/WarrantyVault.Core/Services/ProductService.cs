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

    public bool Delete(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return false;
        }
        
        _products.Remove(product);
        return true;
    }

    public List<ProductValidationError> Update(Guid id, Product updated)
    {   
        var errors = ProductValidator.Validate(updated);
        
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return new List<ProductValidationError>
            {
                ProductValidationError.ProductNotFound   
            };
        }

        if (!errors.Any())
        {
            product.Name = updated.Name;
            product.Brand = updated.Brand;
            product.Model = updated.Model;
        }
        
        return errors;
    }
    
}