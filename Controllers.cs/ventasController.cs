using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;
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
            dbDynamic.Tickets.Add(new Ticket
            {
                FechaTicket=DateTime.Now   
            });
            int idTicket= dbDynamic.Tickets.OrderBy(t=>t.IdTicket).ToList().Max().IdTicket;
            foreach(ventaGeneral venta in ventas)
            {
                if(venta.medicamentos is not null)
                {
                    await dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"EXEC
                    venderMedicamento @idMedicameto={venta.medicamentos.idMedicameto} @cantidad={venta.medicamentos.cantidad}, idTicket={idTicket}");
                }
                if(venta.servicios is not null)
                {
                    dbDynamic.Database.ExecuteSqlAsync($@"EXEC 
                    venderServicio @idServicio={venta.servicios.idServicio} @cantidad={venta.servicios.cantidad}, idTicket={idTicket}");
                }

            }
            return Results.Ok("TDBN");
        }
        catch(Exception Ex)
        {
            return Results.BadRequest(Ex.Message);
        }
    }

}