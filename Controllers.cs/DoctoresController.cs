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
    public async Task<IResult> verDoctores(string token, int? idEspecialidad, int? idDoctor)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        var query=dbDynamic.Doctores.AsQueryable();
        try
        {
            if(idEspecialidad.HasValue)
            {
                query=query.Where(p=>p.IdEspecialidad==idEspecialidad);
            }
            if(idDoctor.HasValue)
            {
                query=query.Where(d=>d.IdDoctor==idDoctor);
            }
            return Results.Ok(query.ToList());
            
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}