using System;
using System.Collections.Generic;

namespace Models;

public partial class BitacoraTicket
{
    public int IdTicket { get; set; }

    public int IdEmpleado { get; set; }

    public int? IdMedicamento { get; set; }

    public int? IdServicio { get; set; }

    public int? CantidadMedicamento { get; set; }

    public int? CantidadServicio { get; set; }

    public string? NombreMedicamento { get; set; }

    public string? NombreServicio { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public decimal TotalTicket { get; set; }

    public DateTime FechaTicket { get; set; }

    public int IdBitacora { get; set; }
}
