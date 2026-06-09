using System;
using System.Collections.Generic;

namespace Models;

public partial class VerHistoriaMedica
{
    public string NombreCompleto { get; set; } = null!;

    public string Curp { get; set; } = null!;

    public string TipoSangre { get; set; } = null!;

    public string? Alergias { get; set; }

    public decimal Peso { get; set; }

    public decimal Estatura { get; set; }

    public string? PadecimientosPrevios { get; set; }

    public int? Edad { get; set; }

    public int IdPaciente { get; set; }
}
