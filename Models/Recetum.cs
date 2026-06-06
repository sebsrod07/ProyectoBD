using System;
using System.Collections.Generic;

namespace Models;

public partial class Recetum
{
    public int FolioReceta { get; set; }

    public string Medicamentos { get; set; } = null!;

    public int FolioCita { get; set; }

    public string? Observaciones { get; set; }

    public DateTime FechaReceta { get; set; }

    public virtual Cita FolioCitaNavigation { get; set; } = null!;
}
