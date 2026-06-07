using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;
using Models;
namespace Controllers;
[ApiController]
public class PacientesController : BaseController
{
    public PacientesController(IConfiguration config) :base(config)
    {
    }
    [HttpGet]
    [Route("/pacientes")]
    public async Task<IResult> GetPacientes(string token, int? idDoc)
    {
        using var dbDynamic=ObtenerContextoDinamico(token,"DOCTOR")??ObtenerContextoDinamico(token,"SECRETARIO");
        var idUsuario=AuthenticationController.sesiones[token].idUsuario;
        try
        {
            if(idDoc.HasValue)
            {
                var citasDoc=dbDynamic.VerCitas.Where(c=>c.IdDoctor==idDoc);
                List<VerNombresPaciente> pacientesDoc=new List<VerNombresPaciente>();
                foreach(VerCita cita in citasDoc)
                {
                    pacientesDoc.Add(
                        new VerNombresPaciente
                        {
                            IdPaciente=cita.IdPaciente,
                            NombreCompleto=cita.Paciente
                        }
                    );
                }
                return Results.Ok(pacientesDoc);
            }
            else
            {
                return Results.Ok(dbDynamic.VerNombresPacientes.OrderBy(p=>p.NombreCompleto).ToList());
            }
            
            
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
            
    }
    [HttpGet]
    [Route("/getIdPaciente")]
    public async Task<IResult> getIdPaciente(string token)
    {
        using var dbDynamic=ObtenerContextoDinamico(token,"PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        var idUsuario=AuthenticationController.sesiones[token].idUsuario;
        var idPaciente= await dbDynamic.Database.SqlQuery<int>($"SELECT dbo.getIdPaciente({idUsuario}) as Value").FirstAsync();
        return Results.Ok(idPaciente);
    }
}