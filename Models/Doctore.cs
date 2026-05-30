using System;
using System.Collections.Generic;

namespace Models;

public partial class Doctore
{
    public int IdDoctor { get; set; }

    public int IdEmpleado { get; set; }

    public int? IdEspecialidad { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Consultorio> Consultorios { get; set; } = new List<Consultorio>();

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Especialidadad? IdEspecialidadNavigation { get; set; }
}
