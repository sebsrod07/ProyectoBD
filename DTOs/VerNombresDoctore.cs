using System;
using System.Collections.Generic;

namespace Models;

public partial class VerNombresDoctore
{
    public int IdEspecialidad { get; set; }

    public int IdEmpleado { get; set; }

    public int IdDoctor { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string NombreEspecialidad { get; set; } = null!;

    public decimal CobroPorConsulta { get; set; }
}
