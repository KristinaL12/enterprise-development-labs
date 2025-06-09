using System;
using System.Collections.Generic;

namespace Backendec2.Models;

/// <summary>
/// Модель, представляющая ячейку склада, по ней строится бд  
/// Содержит информацию о товаре, хранящемся в ячейке, и его количестве.  
/// </summary>

public partial class Cell
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product? Product { get; set; }
}
