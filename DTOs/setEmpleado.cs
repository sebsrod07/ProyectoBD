namespace DTOs;
public class SetEmpleado
{
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int IdEmpleado { get; set; }
    public int Salario { get; set; }
}