using System;
using System.Collections.Generic;

namespace Models;

public partial class VerTicket
{
    public int IdTicket { get; set; }

    public DateTime FechaTicket { get; set; }

    public string TipoArticulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioMedicamento { get; set; }

    public decimal? TotalTicket { get; set; }

    public string Empleado { get; set; } = null!;
}
