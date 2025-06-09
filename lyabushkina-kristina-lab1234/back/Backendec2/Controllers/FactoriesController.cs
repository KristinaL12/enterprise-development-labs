using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backendec2.Models;

namespace Backendec2.Controllers;

/// <summary>
/// Контроллер для работы с производствами. 
/// Предоставляет метод для получения топ производств по объёму поставок за указанный период.
/// Данные используются для анализа производительности производств.
/// </summary>

[ApiController]
[Route("api/factories")]
public class FactoriesController : ControllerBase
{
    private readonly WarehouseContext _context;

    public FactoriesController(WarehouseContext context)
    {
        _context = context;
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopFactories([FromQuery] string start, [FromQuery] string end)
    {
        var result = await (
            from s in _context.Supplies
            join f in _context.Factories on s.FactoryId equals f.Id
            where string.Compare(s.Date, start) >= 0 && string.Compare(s.Date, end) <= 0
            group s by new { f.Name, f.Adrress } into g
            select new
            {
                name = g.Key.Name,
                address = g.Key.Adrress,
                totalQuantity = g.Sum(x => x.Quantity)
            }
        )
        .OrderByDescending(x => x.totalQuantity)
        .ToListAsync();

        return Ok(result);
    }
}