using System;
using System.Collections.Generic;

namespace Models;

public partial class Cobro
{
    public int IdCobro { get; set; }

    public int? IdTicket { get; set; }

    public decimal Total { get; set; }

    public int? IdEmpleado { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Ticket? IdTicketNavigation { get; set; }
}
