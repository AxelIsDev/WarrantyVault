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
    
    [Fact]
    public void Update_ExistingProductWithValidData_UpdatesItAndReturnsNoErrors()
    {
      
        var product = new Product { Name = "Cascos", Brand = "Logitech", Model = "G Pro X 2" };
        service.Add(product);
        var originalId = product.Id;

        var updated = new Product { Name = "Ratón", Brand = "Razer", Model = "DeathAdder" };
        var errors = service.Update(product.Id, updated);
        
        Assert.Empty(errors);                
        Assert.Equal("Ratón", product.Name); 
        Assert.Equal("Razer", product.Brand);
        Assert.Equal("DeathAdder", product.Model);
        Assert.Equal(originalId, product.Id);

    }

    [Fact]
    public void Update_UnknownId_ReturnsProductNotFoundAndKeepsData()
    {
        var product = new Product { Name = "Cascos", Brand = "Logitech", Model = "G Pro X 2" };
        service.Add(product);

        var updated = new Product { Name = "Ratón", Brand = "Razer", Model = "DeathAdder" };
        var unknownId = Guid.NewGuid();

        var errors = service.Update(unknownId, updated);
        Assert.Contains(ProductValidationError.ProductNotFound, errors);
        Assert.Equal("Cascos", product.Name);
    }

    [Fact]
    public void Update_InvalidData_ReturnsErrorsAndKeepsOriginalData()
    {
        var product = new Product { Name = "Cascos", Brand = "Logitech", Model = "G Pro X 2" };
        service.Add(product);

        var updated = new Product { Name = "   ", Brand = "Razer", Model = "DeathAdder" };

        var errors = service.Update(product.Id, updated);

        Assert.Contains(ProductValidationError.NameRequired, errors);
        Assert.Equal("Cascos", product.Name);
        Assert.Equal("Logitech", product.Brand);
        Assert.Equal("G Pro X 2", product.Model);
    }

}