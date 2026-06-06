using System;
using System.Collections.Generic;

namespace Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string Curp { get; set; } = null!;

    public int? IdUsuario { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Persona CurpNavigation { get; set; } = null!;

    public virtual ICollection<HistoriaMedica> HistoriaMedicas { get; set; } = new List<HistoriaMedica>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
