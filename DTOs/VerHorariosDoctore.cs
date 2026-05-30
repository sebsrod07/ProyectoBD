using System;
using System.Collections.Generic;

namespace DTOs;

public partial class VerHorariosDoctore
{
    public TimeOnly HoraIncio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int IdDoctor { get; set; }
}
