using System;
using System.Collections.Generic;

namespace Models;

public partial class Especialidadad
{
    public int IdEspecialidad { get; set; }

    public string NombreEspecialidad { get; set; } = null!;

    public decimal CobroPorConsulta { get; set; }
}
