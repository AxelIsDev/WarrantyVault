namespace WarrantyVault.Web.Models;

public class CreateProductRequest
{
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
}