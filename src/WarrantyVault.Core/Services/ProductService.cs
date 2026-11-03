using WarrantyVault.Core.Models;

namespace WarrantyVault.Core.Services;

public class ProductService
{   private readonly List<Product> _products = new();
    
    public IReadOnlyList<Product>GetAll()
    {
        return _products;
    }
}