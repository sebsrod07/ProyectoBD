using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers;
public class ServiciosController:BaseController
{
    public ServiciosController(IConfiguration confi):base(confi)
    {

    }
    [HttpGet]
    [Route("/servicios/verServicios")]
    public async Task<IResult> verServicios(string token)
    {
        var dbDynamic=ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            var servicios=await dbDynamic.Servicios.OrderBy(s=>s.NombreServicio).ToListAsync();
            return Results.Ok(servicios);
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }

    }
}