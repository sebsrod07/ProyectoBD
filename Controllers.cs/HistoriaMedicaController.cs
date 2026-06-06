using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Controllers;

public class HistoriaMedicaController : BaseController
{
    public HistoriaMedicaController(IConfiguration config):base(config)
    {}
    [HttpPost]
    [Route("/crearHistoria")]
    public async Task<IResult> crearHistoria(string token,[FromBody] HistoriaMedica HM)
    {
        var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            await dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"EXEC crearHistoria @alergias={HM.Alergias}, @tipoSangre={HM.TipoSangre}, 
            @padecimientosPrevios={HM.PadecimientosPrevios}, @peso={HM.Peso}, @estatura={HM.Estatura}, @idPaciente={HM.IdPaciente}");
            return Results.Created();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
        
    }
    [HttpGet]
    [Route("/paciente/verHistoria")]
    public async Task<IResult> verHistorias(string token, int? idPaciente, int? idHistoria, string? CURP, string? Nombre, string? Alergias, decimal? peso, decimal? estatura, string? padecimientosPrevios, int? edad)
    {
        var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE")??
        ObtenerContextoDinamico(token, "DOCTOR");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            var query=dbDynamic.VerHistoriaMedicas.AsQueryable();
            if(!string.IsNullOrEmpty(CURP))
                query=query.Where(HM=>HM.Curp==CURP);
            if(!string.IsNullOrEmpty(Nombre))
                query=query.Where(HM=>HM.NombreCompleto==Nombre);
            if(!string.IsNullOrEmpty(Alergias))
                query=query.Where(HM=>HM.Alergias.Contains(Alergias));
            if(peso.HasValue)
                query=query.Where(HM=>HM.Peso==peso.Value);
            if(estatura.HasValue)
                query=query.Where(HM=>HM.Estatura==estatura);
            if(!string.IsNullOrEmpty(padecimientosPrevios))
                query=query.Where(HM=>HM.PadecimientosPrevios.Contains(padecimientosPrevios));
            if(edad.HasValue)
                query=query.Where(HM=>HM.Edad==edad);
            if(idPaciente.HasValue)
                query=query.Where(HM=>HM.IdPaciente==idPaciente);
            return Results.Ok(query.ToList());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }

    }
    
}
