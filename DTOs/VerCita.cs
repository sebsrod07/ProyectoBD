using System;
using System.Collections.Generic;

namespace Models;

public partial class VerCita
{
    public int IdDoctor { get; set; }

    public string Doctor { get; set; } = null!;

    public string? Paciente { get; set; }

    public DateTime Fecha { get; set; }

    public int FolioCita { get; set; }

    public int IdPaciente { get; set; }

    public string Estatus { get; set; } = null!;

    public int? NumeroConsultorio { get; set; }
}
