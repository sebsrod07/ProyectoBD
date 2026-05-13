using System;
using System.Collections.Generic;

namespace Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string Curp { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Persona CurpNavigation { get; set; } = null!;
}
