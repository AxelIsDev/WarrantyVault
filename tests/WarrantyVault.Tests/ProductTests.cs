using WarrantyVault.Core.Enums;
using WarrantyVault.Core.Models;
using WarrantyVault.Core.Validation;

namespace WarrantyVault.Tests;

public class ProductTests
{
    [Fact]
    public void TwoProducts_ShouldHaveDifferentIds()
    {
        var product1 = new Product
        {   
            Name = "Móvil",
            Brand = "Xiaomi",
            Model = "Redmi Note 12 Pro"
        };
        
        var product2 = new Product
        {   
            Name = "Auriculares",
            Brand = "Apple",
            Model = "AirPods pro"
        };
        
        Assert.NotEqual(product1.Id, product2.Id);
    }
    
    [Fact]
    public void Validate_NameIsWhitespace_ReturnsNameRequiredError()
    {
        var product = new Product
        {
            Name = " ",
            Brand = "Nintendo",
            Model = "OLED"
        };
        
        var errors = ProductValidator.Validate(product);
        
        Assert.Contains(ProductValidationError.NameRequired, errors);
    }
    
    [Fact]
    public void Validate_BrandIsWhitespace_ReturnsBrandRequiredError()
    {

        var product = new Product
        {
            Name = "Cascos",
            Brand = " ",
            Model = "LG"
        };

        var errors = ProductValidator.Validate(product);

        Assert.Contains(ProductValidationError.BrandRequired, errors);
    }
    
    [Fact]
    public void Validate_ModelIsWhitespace_ReturnsModelRequiredError()
    {
        var product = new Product
        {
            Name = "Ratón gaming",
            Brand = "Corsair",
            Model = " "
        };

        var errors = ProductValidator.Validate(product);

        Assert.Contains(ProductValidationError.ModelRequired, errors);
    }
    
    [Fact]
    public void Validate_AllFieldsFilled_ReturnsNoErrors()
    {
        var product = new Product
        {
            Name = "PC",
            Brand = "HP",
            Model = "ZBook"
        };
        
        var errors = ProductValidator.Validate(product);

        Assert.Empty(errors);
    }
    
    [Fact]
    public void Validate_AllFieldsWhitespace_ReturnsAllThreeErrors()
    {
        var product = new Product
        {
            Name = "   ",
            Brand = "   ",
            Model = "   "
        };
        
        var errors = ProductValidator.Validate(product);
        
        Assert.Contains(ProductValidationError.NameRequired, errors);
        Assert.Contains(ProductValidationError.BrandRequired, errors);
        Assert.Contains(ProductValidationError.ModelRequired, errors);
    }
    
}