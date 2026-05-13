using System;
using System.Collections.Generic;

namespace Models;

public partial class Consultorio
{
    public int NumeroConsultorio { get; set; }

    public int IdDoctor { get; set; }

    public DateTime HoraOcupacion { get; set; }

    public DateTime HoraLiberacion { get; set; }

    public virtual Doctore IdDoctorNavigation { get; set; } = null!;
}
