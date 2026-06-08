using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Controllers;
public class ventasController:BaseController
{
    public ventasController(IConfiguration config):base(config)
    {

    }
    [HttpPost]
    [Route("/secretario/ventas")]
    public async Task<IResult> hacerVenta(string token, [FromBody] List<ventaGeneral> ventas)
    {
        var  dbDynamic= ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            foreach(ventaGeneral venta in ventas)
            {
                if(venta.medicamentos.HasValue)
                {
                    dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"EXEC
                    venderMEdicamento @idMedicameto={venta.medicamentos.idMedicameto} @cantidad={venta.medicamentos.cantidad}");
                }
                dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"")

            }

        }
    }

}