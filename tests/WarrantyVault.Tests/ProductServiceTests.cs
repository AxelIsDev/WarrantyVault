using WarrantyVault.Core.Services;

namespace WarrantyVault.Tests;

public class ProductServiceTests
{
    [Fact]
    public void GetAll_NewService_ReturnsEmptyList()
    {
        // Arrange
        var service = new ProductService();

        // Act
        var products = service.GetAll();

        // Assert
        Assert.Empty(products);
    }
}