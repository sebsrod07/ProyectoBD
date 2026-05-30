using System;
using System.Collections.Generic;

namespace Models;

public partial class VerNombresPaciente
{
    public int IdPaciente { get; set; }

    public string NombreCompleto { get; set; } = null!;
}
