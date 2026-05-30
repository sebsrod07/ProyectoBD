using System;
using System.Collections.Generic;

namespace Models;

public partial class VerHorariosDoctore
{
    public TimeOnly HoraIncio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int IdDoctor { get; set; }
}
