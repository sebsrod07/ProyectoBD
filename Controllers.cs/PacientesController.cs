using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;
using Models;
namespace Controllers;
[ApiController]
public class PacientesController : BaseController
{
    private readonly IConfiguration _config;
    public PacientesController(IConfiguration config) :base(config)
    {
    }
    [HttpGet]
    [Route("/pacientes")]
    public async Task<IResult> GetPacientes(string token)
    {
        using var dbDynamic=ObtenerContextoDinamico(token,"DOCTOR");
        var idUsuario=AuthenticationController.sesiones[token].idUsuario;
        try
        {
            int idDoctor = await dbDynamic.Database.SqlQuery<int>($"SELECT dbo.getIdDoctor({idUsuario}) as value").FirstAsync();
            var citas =await dbDynamic.Database.SqlQuery<VerCita>($"SELECT * FROM VerCitas where idDoctor={idDoctor}").ToListAsync();
            return Results.Ok(citas);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
            
    }
}