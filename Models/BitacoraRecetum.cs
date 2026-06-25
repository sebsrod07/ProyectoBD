using System;
using System.Collections.Generic;

namespace Models;

public partial class BitacoraRecetum
{
    public int IdBit { get; set; }

    public int Folioreceta { get; set; }

    public int FolioCita { get; set; }

    public DateTime FechaReceta { get; set; }

    public string NomPaciente { get; set; } = null!;

    public string? NomDoctor { get; set; }
}
