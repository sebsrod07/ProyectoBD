using System;
using System.Collections.Generic;

namespace Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public DateTime FechaContratacion { get; set; }

    public int? IdHorario { get; set; }

    public int Salario { get; set; }

    public int? IdUsuario { get; set; }

    public string? Estatus { get; set; }

    public string? Curp { get; set; }

    public virtual ICollection<Cobro> Cobros { get; set; } = new List<Cobro>();

    public virtual Persona? CurpNavigation { get; set; }

    public virtual ICollection<Doctore> Doctores { get; set; } = new List<Doctore>();

    public virtual Horario? IdHorarioNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
