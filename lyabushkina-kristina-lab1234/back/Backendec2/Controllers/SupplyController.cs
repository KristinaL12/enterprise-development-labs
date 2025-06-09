using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backendec2.Models;

namespace Backendec2.Controllers;

/// <summary>
/// Контроллер для работы с поставками. 
/// Предоставляет метод для получения данных о поставках.
/// </summary>

[ApiController]
[Route("api/supply")]
public class SupplyController : ControllerBase
{
    private readonly WarehouseContext _context;

    public SupplyController(WarehouseContext context)
    {
        _context = context;
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetSupplyStats()
    {
        var stats = await _context.Supplies
            .Include(s => s.Factory)
            .Include(s => s.Product)
            .GroupBy(s => new
            {
                FactoryName = s.Factory!.Name,
                ProductName = s.Product!.Name
            })
            .Select(g => new
            {
                FactoryName = g.Key.FactoryName,
                ProductName = g.Key.ProductName,
                Quantity = g.Sum(s => s.Quantity)
            })
            .ToListAsync();

        return Ok(stats);
    }
}