using System;
using System.Collections.Generic;

namespace Models;

public partial class Horario
{
    public int IdHorario { get; set; }

    public TimeOnly HoraIncio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
