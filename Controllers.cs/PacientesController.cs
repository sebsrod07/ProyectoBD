using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;
[ApiController]
public class PacientesController : ControllerBase
{
    private readonly IConfiguration _config;
    public PacientesController(
        IConfiguration config)
    {
        _config = config;
    }
    [HttpGet]
    [Route("/pacientes")]
    public async Task<IResult> GetPacientes(string token)
    {
        if(!AuthenticationController.sesiones.ContainsKey(token))
            return Results.Unauthorized();
        string rol=AuthenticationController.sesiones[token].permiso;
        string cs=null;
        if(rol.ToUpper()=="DOCTOR")
            cs=_config.GetConnectionString("DoctorCnecction");
        else
            return Results.Unauthorized();
        var options =
        new DbContextOptionsBuilder<ProyectoBdV1Context>()
        .UseSqlServer(cs)
        .Options;
        using var dbDynamic =
            new ProyectoBdV1Context(options);
        var idUsuario=AuthenticationController.sesiones[token].idUsuario;
        try
        {
        int idDoctor = await dbDynamic.Database.SqlQuery<int>($"SELECT dbo.getIdDoctor({idUsuario}) as value").FirstAsync();
        var citas =await dbDynamic.Database.SqlQuery<VerCitasResult>($"SELECT * FROM VerCitas({idDoctor})").ToListAsync();
        return Results.Ok(citas);

        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
            
    }
}