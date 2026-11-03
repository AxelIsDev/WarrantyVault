namespace WarrantyVault.Core.Models;

public class Product
{   
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
}