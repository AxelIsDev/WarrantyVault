using WarrantyVault.Core.Enums;
using WarrantyVault.Core.Models;
using WarrantyVault.Core.Services;

namespace WarrantyVault.Tests;

public class ProductServiceTests
{
    private ProductService service;
    public ProductServiceTests()
    {
        service = new ProductService();
    }
    
    [Fact]
    public void GetAll_NewService_ReturnsEmptyList()
    {
        var products = service.GetAll();
        
        Assert.Empty(products);
    }
    
    [Fact]
    public void Add_ValidProduct_IsStoredAndReturnsNoErrors()
    {
        var product = new Product
        {
            Name = "Cascos",
            Brand = "Logitech",
            Model = "G Pro X 2"
        };

        var errors = service.Add(product);
        
        Assert.Empty(errors);
        Assert.Contains(product,service.GetAll());
        
    }
    
    [Fact]
    public void Add_InvalidProductNoName_IsNotStoredAndReturnsErrors()
    {
        var product = new Product
        {
            Name = " ",
            Brand = "Logitech",
            Model = "G Pro X 2"
        };

        var errors = service.Add(product);
        
        Assert.Contains(ProductValidationError.NameRequired, errors);
    }
    
    [Fact]
    public void Add_NullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        Product product = null!;

        // Act
        var action = () => service.Add(product);

        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }
}