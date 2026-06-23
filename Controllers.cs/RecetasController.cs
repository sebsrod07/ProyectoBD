using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers;
public class RecetasController:BaseController
{
    public RecetasController(IConfiguration config) :base(config)
    {}
    [HttpPost]
    [Route("/doctores/crearReceta")]
    public async Task<IResult> crearReceta(string token, string Medicamentos, int folioCita, string Observaciones)
    {
        var dbDynamic=ObtenerContextoDinamico(token, "DOCTOR");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            await dbDynamic.Database.ExecuteSqlInterpolatedAsync($"EXEC crearReceta @medicamentosList={Medicamentos}, @observacionesList={Observaciones}, @folioCita={folioCita}");
            return Results.Ok("Creada Correctamente");
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("/doctores/verRecetas")]
    public async Task<IResult> verRecetas(string token, int? idDoctor, int? idPaciente)
    {
        var dbDynamic=ObtenerContextoDinamico(token, "DOCTOR");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            var query=dbDynamic.VerRecetas.AsQueryable();
            if(idDoctor.HasValue)
                query=query.Where(R=>R.IdDoctor==idDoctor);
            if(idPaciente.HasValue)
                query=query.Where(R=>R.IdPaciente==idPaciente);
            return Results.Ok(query.ToList());
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
        
    }
}