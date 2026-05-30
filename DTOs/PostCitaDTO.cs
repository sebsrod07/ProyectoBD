using Models;
using Controllers;
namespace DTOs;

public class postCitasDTO
{
    public DateTime FechaCita {get;set;}
    public int idPaciente {get;set;}
    public int idDoctor {get;set;}
}