using System;
using System.Collections.Generic;

namespace Models;

public partial class Ticket
{
    public int IdTicket { get; set; }

    public DateTime FechaTicket { get; set; }

    public decimal? TotalTicket { get; set; }
}
