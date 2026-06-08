namespace Controllers;
public class ServiciosController:BaseController
{
    public ServiciosController(IConfiguration confi):base(confi)
    {

    }
    [HttpGet]
    [Route("/servicios/verServicios")]
    public async task<IResult> verServicios(string token)
    {
        var dbDynamic=ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            return Results.ok(dbDynamic.Servicios.OrderBy(s=>s.NombreServicio).ToList());
        }
        catch(Exception ex)
        {
            return Results.badRequest(ex.message);
        }

    }
}