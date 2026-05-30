using System;
using System.Collections.Generic;

namespace Models;

public partial class Ticket
{
    public int IdTicket { get; set; }

    public int? IdServicio { get; set; }

    public int? NumeroServicios { get; set; }

    public DateTime FechaTicket { get; set; }

    public int? NumeroMedicamentos { get; set; }

    public int? IdMedicamento { get; set; }

    public virtual ICollection<Cobro> Cobros { get; set; } = new List<Cobro>();

    public virtual Medicamento? IdMedicamentoNavigation { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
