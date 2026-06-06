using System;
using System.Collections.Generic;

namespace Models;

public partial class VerReceta
{
    public string Doctor { get; set; } = null!;

    public string Paciente { get; set; } = null!;

    public int FolioReceta { get; set; }

    public int IdDoctor { get; set; }

    public int IdPaciente { get; set; }

    public string? Medicamentos { get; set; }

    public string? Observaciones { get; set; }
}
