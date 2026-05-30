using System;
using System.Collections.Generic;

namespace Models;

public partial class VerNombresDoctore
{
    public int IdEmpleado { get; set; }

    public int IdDoctor { get; set; }

    public string NombreCompleto { get; set; } = null!;
    public string nombreEspecialidad{get;set;} = null!;
    public decimal cobroPorConsulta {get;set;}
}
