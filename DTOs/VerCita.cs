using System;
using System.Collections.Generic;

namespace DTOs;

public partial class VerCita
{
    public int IdDoctor { get; set; }

    public string Doctor { get; set; } = null!;

    public string Paciente { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int FolioCita { get; set; }

    public int IdPaciente { get; set; }
}
