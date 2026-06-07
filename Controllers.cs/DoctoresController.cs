using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.ObjectPool;
namespace Controllers;
[ApiController]
public class DoctoresController:BaseController
{
    public DoctoresController(IConfiguration config) : base(config)
    {
    }
    [HttpGet]
    [Route("/getDoctores")]
    public async Task<IResult> verDoctores(string token, int? idEspecialidad, bool? horario, int? idDoc)
    {
        using var dbDynamic = ObtenerContextoDinamico(token, "PACIENTE");
        if (dbDynamic is null)
            return Results.Unauthorized();

        try
        {
            bool verHorario = horario ?? false;

            if (idEspecialidad.HasValue && verHorario && idDoc.HasValue)
            {
                var hora = await dbDynamic.VerNombresDoctores
                    .Where(d => d.IdEspecialidad == idEspecialidad && d.IdDoctor == idDoc)
                    .Select(d => new { d.HoraIncio, d.HoraFin })
                    .FirstOrDefaultAsync(); 
                if (hora == null)
                    return Results.NotFound("No se encontraron horarios para este doctor.");
                return Results.Ok($"Horas Disponibles: {hora.HoraIncio} - {hora.HoraFin}");
            }

            var query = dbDynamic.VerNombresDoctores.AsQueryable();
            if (idEspecialidad.HasValue)
            {
                query = query.Where(d => d.IdEspecialidad == idEspecialidad);
            }
            if(idDoc.HasValue)
            {
                query=query.Where(d=>d.IdDoctor==idDoc);
            }
            var doctores = await query
                .OrderBy(d => d.NombreEspecialidad)
                .ToListAsync();
            if(doctores.Count()==0)
                return Results.NotFound("No Existen Doctores en este filtro");
            return Results.Ok(doctores);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("/getIdDoctor")]
    public async Task<IResult> getIdDoctor(string token)
    {
        var dbDynamic = ObtenerContextoDinamico(token, "DOCTOR");
        if (dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            int idUserDoc= dbDynamic.Database.SqlQuery<int>($"select dbo.getIdDoctor({AuthenticationController.sesiones[token].idUsuario}) as Value").First();
            return Results.Ok(idUserDoc);
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
        
    }
    
}