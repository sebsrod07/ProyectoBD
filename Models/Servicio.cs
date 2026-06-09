using System;
using System.Collections.Generic;

namespace Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string NombreServicio { get; set; } = null!;

    public string EstatusServicio { get; set; } = null!;

    public decimal PrecioServicio { get; set; }

    public virtual ICollection<TicketServicio> TicketServicios { get; set; } = new List<TicketServicio>();
}
