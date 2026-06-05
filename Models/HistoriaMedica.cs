using System;
using System.Collections.Generic;

namespace Models;

public partial class HistoriaMedica
{
    public int IdHistoriaMedica { get; set; }

    public string TipoSangre { get; set; } = null!;

    public int IdPaciente { get; set; }

    public string? Alergias { get; set; }

    public string? PadecimientosPrevios { get; set; }

    public decimal Peso { get; set; }

    public decimal Estatura { get; set; }
}
