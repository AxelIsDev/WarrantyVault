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
        Assert.Empty(service.GetAll());
    }
    
    [Fact]
    public void Add_NullProduct_ThrowsArgumentNullException()
    {
        Product product = null!;

        var action = () => service.Add(product);
        
        Assert.Throws<ArgumentNullException>(action);
    }
    
    [Fact]
    public void Delete_ExistingProduct_RemovesItAndReturnsTrue()
    {
        var product = new Product
        {
            Name = "Silla Gaming",
            Brand = "NewSkill",
            Model = "Takamikura"
        };
        
        service.Add(product);
        
        var result = service.Delete(product.Id);
        
        Assert.True(result);
        
        Assert.DoesNotContain(product, service.GetAll());
    }
    
    [Fact]
    public void Delete_UnknownId_ReturnsFalseAndKeepsList()
    {
        var product = new Product
        {   
            Name = "Videojuego",
            Brand = "Nintendo",
            Model = "The Legend Of Zelda: Ocarina Of Time Remake"
        };
        
        service.Add(product);
        
        var result = service.Delete(Guid.NewGuid());
        
        Assert.False(result);

        Assert.Single(service.GetAll());
    }
}