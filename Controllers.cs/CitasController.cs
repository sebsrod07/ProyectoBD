using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;
using Microsoft.Data.SqlClient;
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
            return Results.Unauthorized(); 
        try
        {
            var res=await dbDynamic.Database.ExecuteSqlInterpolatedAsync($" EXEC InsertarCita @idDoctor={cita.idDoctor}, @idPaciente={cita.idPaciente}, @fecha={cita.FechaCita}");
            return Results.Ok("CREADA");
        }
        catch (SqlException ex)
        {
            return Results.BadRequest(new {mensaje=ex.Message});
        }
        
    }
}