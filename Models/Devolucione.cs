using System;
using System.Collections.Generic;

namespace Models;

public partial class Devolucione
{
    public int FolioDevolucion { get; set; }

    public int FolioPago { get; set; }

    public DateTime FechaDevolucion { get; set; }

    public decimal MontoDevuelto { get; set; }

    public virtual Pago FolioPagoNavigation { get; set; } = null!;
}
