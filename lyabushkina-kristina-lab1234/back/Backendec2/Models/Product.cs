using System;
using System.Collections.Generic;

namespace Backendec2.Models;

/// <summary>
/// Модель, представляющая товар, по ней строится бд  
/// Содержит информацию о товаре.  
/// </summary>
public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public int CellId { get; set; }

    public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
