using System;
using System.Collections.Generic;

namespace Models;

public partial class Pago
{
    public int FolioCita { get; set; }

    public string EstatusPago { get; set; } = null!;

    public decimal? MontoPagado { get; set; }

    public int FolioPago { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual Cita FolioCitaNavigation { get; set; } = null!;
}
