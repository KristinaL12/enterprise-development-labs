using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backendec2.Models;

namespace Backendec2.Controllers;

/// <summary>
/// Контроллер для работы с товарами. 
/// Предоставляет метод для получения загруженности склада.
/// </summary>

[ApiController]
[Route("api/warehouse")]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseContext _context;

    public WarehouseController(WarehouseContext context)
    {
        _context = context;
    }

    [HttpGet("state")]
    public async Task<IActionResult> GetWarehouseState()
    {
        var result = await _context.Cells
            .Include(c => c.Product)
            .Where(c => c.Product != null)
            .Select(c => new {
                cellId = c.Id,
                productName = c.Product!.Name,
                quantity = c.Quantity ?? 0
            })
            .ToListAsync();

        return Ok(result);
    }
}