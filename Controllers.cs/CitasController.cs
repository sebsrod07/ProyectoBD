using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
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
            return Results.BadRequest( ex.Message);
        }
        
    }
    [HttpGet]
    [Route("/verCitas")]
    public async Task<IResult> verCita(string token, int? idPaciente, DateOnly? fechaF, string? estatusF )
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        var query=dbDynamic.VerCitas.AsQueryable();
        try
        {
            if(fechaF.HasValue)
                query=query.Where(c=>c.Fecha.Day==fechaF.Value.Day && c.Fecha.Month==fechaF.Value.Month && c.Fecha.Year==fechaF.Value.Year);
            if(!string.IsNullOrEmpty(estatusF))
                query=query.Where(c=>c.Estatus==estatusF);
            if(idPaciente.HasValue)
                query=query.Where(c=>c.IdPaciente==idPaciente);
            return Results.Ok(query.ToList());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
        

        
    }
    [HttpPut]
    [Route("/cancelarCita")]
    public async Task<IResult> cancelaCita(string token, int folioCita, int idPaciente)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            dbDynamic.Database.ExecuteSqlInterpolated($" EXEC cancelarCita @folioCita={folioCita}, @idPaciente={idPaciente}");
            var dev=await dbDynamic.Database.SqlQuery<decimal?>($"select dbo.calculaReembolosPaciente({folioCita}) as Value").FirstOrDefaultAsync();
            if(dev is null)
                return Results.Ok("CITA CANCELADA, NO APLICA REEMBOLSO");
            return Results.Ok($"CITA CANCELADA. SU REMBOLSO ES DE {dev}");
            
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpPut]
    [Route("/pagarCita")]
    public async Task<IResult> pagarCita(string token, int folioCita)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            await dbDynamic.Database.ExecuteSqlInterpolatedAsync($"exec pagarCita {folioCita}");
            return Results.Ok("PAGADA");
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
            
    }
}