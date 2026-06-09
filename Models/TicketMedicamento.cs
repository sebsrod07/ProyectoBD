using System;
using System.Collections.Generic;

namespace Models;

public partial class TicketMedicamento
{
    public int IdMedicamento { get; set; }

    public int IdTicket { get; set; }

    public int Cantidad { get; set; }

    public virtual Medicamento IdMedicamentoNavigation { get; set; } = null!;
}
