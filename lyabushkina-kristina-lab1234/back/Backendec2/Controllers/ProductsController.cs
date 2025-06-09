using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backendec2.Models;

namespace Backendec2.Controllers;

/// <summary>
/// Контроллер для работы с товарами. 
/// Предоставляет метод для получения всех товаров, заказанных товаров и топа товаров.
/// </summary>

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly WarehouseContext _context;

    public ProductsController(WarehouseContext context)
    {
        _context = context;
    }

    [HttpGet("ordered")]
    public async Task<IActionResult> GetAllOrdered()
    {
        var products = await _context.Products
            //.OrderBy(p => p.Name)
            //.Select(p => new {
            //    name = p.Name,
            //    quantity = p.Quantity
            //})
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] string date, [FromQuery] string org)
    {
        var result = await _context.Supplies
            .Include(s => s.Product)
                .ThenInclude(p => p.Cells)
            //.Include(s => s.Product!.Cells)
            .Include(s => s.Factory)
            .Where(s => s.Date == date && s.Factory!.Name!.ToLower() == org.ToLower())
            .Select(s => new {
                name = s.Product!.Name,
                quantity = s.Quantity
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("top5")]
    public async Task<IActionResult> GetTop5()
    {
        var result = await _context.Products
            .OrderByDescending(p => p.Quantity)
            .Take(5)
            .Select(p => new {
                name = p.Name,
                quantity = p.Quantity
            })
            .ToListAsync();

        return Ok(result);
    }
}