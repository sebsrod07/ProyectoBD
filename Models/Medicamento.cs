using System;
using System.Collections.Generic;

namespace Models;

public partial class Medicamento
{
    public int IdMedicamento { get; set; }

    public string NombreMedicamento { get; set; } = null!;

    public int CantidadEnStock { get; set; }

    public string Tratamiento { get; set; } = null!;

    public decimal PrecioMedicamento { get; set; }

    public virtual ICollection<TicketMedicamento> TicketMedicamentos { get; set; } = new List<TicketMedicamento>();
}
