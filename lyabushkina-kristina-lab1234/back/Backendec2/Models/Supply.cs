using System;
using System.Collections.Generic;

namespace Backendec2.Models;

/// <summary>
/// Модель, представляющая поставки, по ней строится бд  
/// Содержит информацию о поставках, содержит поля для навигации(упрощает обращение к свойствам классов).  
/// </summary>
public partial class Supply
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int FactoryId { get; set; }

    public string Date { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
    public virtual Factory Factory { get; set; } = null;
}
