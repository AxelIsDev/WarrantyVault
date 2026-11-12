using Microsoft.AspNetCore.Mvc;
using WarrantyVault.Core.Enums;
using WarrantyVault.Core.Models;
using WarrantyVault.Core.Services;
using WarrantyVault.Web.Models;

namespace WarrantyVault.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public ActionResult<IReadOnlyList<Product>> GetAll()
    {
        return Ok(_service.GetAll());
    }
    
    [HttpPost]
    public ActionResult<Product> Create(ProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Brand = request.Brand,
            Model = request.Model
        };
        
        var errors = _service.Add(product);
        
        if (errors.Any())
        {
            return BadRequest(errors);
        }
        
        return StatusCode(201, product);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _service.Delete(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, ProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Brand = request.Brand,
            Model = request.Model
        };
        
        var errors = _service.Update(id, product);
        
        if (errors.Contains(ProductValidationError.ProductNotFound))
        {
            return NotFound(errors);
        }

        if (errors.Any())
        {
            return BadRequest(errors);
        }

        return NoContent();
    }
}