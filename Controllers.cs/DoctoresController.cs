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
    public async Task<IResult> verDoctores(string token, int? idEspecialidad)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            if(idEspecialidad is null)
            {
                var doctores = await dbDynamic.VerNombresDoctores.FromSqlRaw("select * from verNombresDoctores order by nombreEspecialidad").ToListAsync();
                return Results.Ok(doctores);
            }
            else
            {
                var doctores = await dbDynamic.VerNombresDoctores.FromSqlInterpolated($"select * from verNombresDoctores where idEspecialidad={idEspecialidad} order by nombreEspecialidad").ToListAsync();
                return Results.Ok(doctores);
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}