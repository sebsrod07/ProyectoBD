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
        var nuevoTicket = new Ticket
        {
            FechaTicket = DateTime.Now   
        };
        
        dbDynamic.Tickets.Add(nuevoTicket);
        await dbDynamic.SaveChangesAsync();
        int idTicket = nuevoTicket.IdTicket;
        try
        {
            
            foreach(ventaGeneral venta in ventas)
            {
                if(venta.medicamentos is not null)
                {
                    await dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"EXEC
                    venderMedicamento @idMedicamento={venta.medicamentos.idmedicamento}, @cantidad={venta.medicamentos.cantidad}, @idTicket={idTicket}");
                }
                if(venta.servicios is not null)
                {
                    await dbDynamic.Database.ExecuteSqlAsync($@"EXEC 
                    venderServicio @idServicio={venta.servicios.idServicio}, @cantidad={venta.servicios.cantidad}, @idTicket={idTicket}");
                }
            }
            var ticket = await dbDynamic.Tickets.FindAsync(idTicket);
            ticket.TotalTicket=await dbDynamic.Database.SqlQuery<decimal>($"select  dbo.calculaTotal({idTicket}) as Value").FirstOrDefaultAsync();
            await dbDynamic.SaveChangesAsync();
            return Results.Ok("Venta Registrada Correctamente");
        }
        catch(Exception Ex)
        {
            var ticketFallido = await dbDynamic.Tickets.FindAsync(idTicket);
            if (ticketFallido != null)
            {
                ticketFallido.TotalTicket = 0; 
                await dbDynamic.SaveChangesAsync(); 
            }
            return Results.BadRequest(Ex.Message);
        }
    }
    [HttpPost]
    [Route("/ventas/visualizarVenta")]
    public async Task<IResult> verVenta(string token,[FromBody] List<ventaGeneral> ventas)
    {
        var dbDynamic = ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        decimal total=0;
        decimal precio;
        foreach(ventaGeneral venta in ventas)
        {
            if(venta.medicamentos is not null)
            {
                precio=await dbDynamic.Medicamentos.Where(m=>m.IdMedicamento==venta.medicamentos.idmedicamento).Select(m=>m.PrecioMedicamento).FirstAsync();
                total=total+venta.medicamentos.cantidad * precio;
            }
            if(venta.servicios is not null)
            {
                precio= await dbDynamic.Servicios.Where(m=>m.IdServicio==venta.servicios.idServicio).Select(m=>m.PrecioServicio).FirstAsync();
                total=total+venta.servicios.cantidad * precio;
            }
        }
        return Results.Ok(total);
    }

}