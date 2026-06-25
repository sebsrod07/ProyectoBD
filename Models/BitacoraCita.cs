using System;
using System.Collections.Generic;

namespace Models;

public partial class BitacoraCita
{
    public int IdDoctor { get; set; }

    public int IdPaciente { get; set; }

    public int FolioCita { get; set; }

    public string Paciente { get; set; } = null!;

    public string Doctor { get; set; } = null!;

    public string Especialidad { get; set; } = null!;

    public int IdEspecialidad { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public int IdBitacora { get; set; }

    public string Estatus { get; set; } = null!;
}
