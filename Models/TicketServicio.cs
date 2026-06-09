using System;
using System.Collections.Generic;

namespace Models;

public partial class TicketServicio
{
    public int IdServicio { get; set; }

    public int IdTicket { get; set; }

    public int Cantidad { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
