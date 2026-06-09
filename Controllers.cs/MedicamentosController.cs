using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Controllers;
public class MedicamentosController:BaseController
{
    public MedicamentosController(IConfiguration config):base(config)
    {}
    [HttpGet]
    [Route("/secretario/getMedicamentos")]
    public async Task<IResult> getMedicamentos(string token)
    {
        var dbDynamic = ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();

        try
        {
            var medicamentos = await dbDynamic.Medicamentos.OrderBy(m=>m.NombreMedicamento).ToListAsync();
            return Results.Ok(medicamentos);
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}