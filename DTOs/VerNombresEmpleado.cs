using System;
using System.Collections.Generic;

namespace Models;

public partial class VerNombresEmpleado
{
    public int IdEmpleado { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string? Estatus { get; set; }

    public DateTime FechaContratacion { get; set; }

    public decimal Salario { get; set; }
}
