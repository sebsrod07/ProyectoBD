using System;
using System.Collections.Generic;

namespace Models;

public partial class Consultorio
{
    public int NumeroConsultorio { get; set; }

    public int IdDoctor { get; set; }

    public TimeOnly HoraOcupacion { get; set; }

    public TimeOnly HoraLiberacion { get; set; }

    public DateOnly? FechaAsignacion { get; set; }

    public virtual Doctore IdDoctorNavigation { get; set; } = null!;
}
