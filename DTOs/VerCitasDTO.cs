namespace DTOs;
public class VerCitasResult
{
    public int idPaciente { get; set; }

    public string Paciente { get; set; }

    public int idDoctor { get; set; }

    public string Doctor { get; set; }

    public DateTime fecha { get; set; }

    public string estatus { get; set; }
}