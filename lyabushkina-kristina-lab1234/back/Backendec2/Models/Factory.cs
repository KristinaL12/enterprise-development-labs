using System;
using System.Collections.Generic;

namespace Backendec2.Models;

/// <summary>
/// Модель, представляющая производство, по ней строится бд  
/// Содержит информацию о производстве.  
/// </summary>

public partial class Factory
{
    public int Id { get; set; }

    public string? Adrress { get; set; }

    public string? Name { get; set; }
}
