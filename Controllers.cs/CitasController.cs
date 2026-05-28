using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;
using Funciones;
namespace Controllers;
[ApiController]
public class CitasController:BaseController
{
    public CitasController(IConfiguration config) : base(config)
    {
    }

    [HttpPost]
    [Route("/generarcita")]
    public async Task<IResult> crearCita(string token, postCitasDTO cita)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic ==null)
            Results.Unauthorized(); 
        try
        {
            dbDynamic.Citas.FromSqlInterpolated($" EXEC ValidarCita @idDoctor={cita.idDoctor}, @idPaciente={cita.idPaciente}, @fecha={cita.FechaCita}");
            return Results.Ok("Creada");
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
        
    }
}