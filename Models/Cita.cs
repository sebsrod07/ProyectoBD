using System;
using System.Collections.Generic;

namespace Models;

public partial class Cita
{
    public int FolioCita { get; set; }

    public int IdDoctor { get; set; }

    public int IdPaciente { get; set; }

    public DateTime Fecha { get; set; }

    public string Estatus { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual Doctore IdDoctorNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
