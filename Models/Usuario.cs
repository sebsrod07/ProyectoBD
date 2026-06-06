using System;
using System.Collections.Generic;

namespace Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    /// <summary>
    /// mail con el que se autoriza
    /// </summary>
    public string NombreUsuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    /// <summary>
    /// rol que desempeña al usar la bd
    /// </summary>
    public string Permiso { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
