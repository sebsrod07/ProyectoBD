using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;
using Microsoft.Data.SqlClient;
using Models;
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
    public async Task<IResult> verCita(string token, int? idPaciente, DateOnly? fechaF, string? estatusF, int? idDoc )
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE") ?? ObtenerContextoDinamico(token, "DOCTOR") ?? ObtenerContextoDinamico(token, "SECRETARIO");
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
            if(idDoc.HasValue)
                query=query.Where(c=>c.IdDoctor==idDoc);
            return Results.Ok(query.OrderBy(c=>c.Fecha).ToList());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
        

        
    }
    [HttpPut]
    [Route("/cancelarCita")]
    public async Task<IResult> cancelaCita(string token, int folioCita, int idPaciente, bool cancelaDoc)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE") ?? ObtenerContextoDinamico(token, "DOCTOR") ?? ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            if(!cancelaDoc)
            {
                dbDynamic.Database.ExecuteSqlInterpolated($" EXEC cancelarCita @folioCita={folioCita}, @idPaciente={idPaciente}, @cancelaDoc={0}");
                var dev=await dbDynamic.Database.SqlQuery<decimal?>($"select dbo.calculaReembolosPaciente({folioCita},0) as Value").FirstOrDefaultAsync();
                if(dev is null)
                    return Results.Ok("CITA CANCELADA, NO APLICA REEMBOLSO");
                return Results.Ok($"CITA CANCELADA. SU REMBOLSO ES DE {dev}");
            }
            else
            {
                dbDynamic.Database.ExecuteSqlInterpolated($" EXEC cancelarCita @folioCita={folioCita}, @idPaciente={idPaciente}, @cancelaDoc={1}");
                var dev=await dbDynamic.Database.SqlQuery<decimal?>($"select dbo.calculaReembolosPaciente({folioCita},0) as Value").FirstOrDefaultAsync();
                if(dev is null)
                    return Results.Ok("CITA CANCELADA, NO APLICA REEMBOLSO");
                return Results.Ok($"CITA CANCELADA. SU REMBOLSO ES DE {dev}");
            }
                
            
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
    [HttpPut]
    [Route("/finalizarCita")]
    public async Task<IResult> finalizarCita(string token, int folioCita, int idDoc)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "DOCTOR");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            if(dbDynamic.Citas.FirstOrDefault(C=>C.FolioCita==folioCita && C.IdDoctor==idDoc) is null)
                throw new Exception ("No existe esta cita");
            var citaAfinalizar= dbDynamic.Citas.First(C=>C.FolioCita==folioCita && C.IdDoctor==idDoc);


            if(citaAfinalizar.Fecha>DateTime.Now)
                throw new Exception ("Esta cita aun no sucede");
            if(citaAfinalizar.Estatus=="CANCELADA")
                throw new Exception("Esta cita esta cancelada");
            if(citaAfinalizar.Estatus=="FINALIZADA")
                throw new Exception("Esta cita ya esta finalizada");
            if(citaAfinalizar.Estatus=="PENDIENTE DE PAGO"||citaAfinalizar.Estatus=="PENDIENTE DE ATENCION")
                throw new Exception("No puedes cancelar esta cita");
            citaAfinalizar.Estatus="FINALIZADA";
            await dbDynamic.SaveChangesAsync();
            return Results.Ok("ACTUALIZADA");
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
            
    }
}