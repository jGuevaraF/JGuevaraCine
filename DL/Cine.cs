using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCine { get; set; }

    public string CineNombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public decimal Ventas { get; set; }

    public int? IdZona { get; set; }

    public string? ZonaNombre { get; set; }
    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }
}
