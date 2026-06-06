using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;
public class NotificacionController:BaseController
{
    public NotificacionController(IConfiguration config):base(config)
    {
    }
    [HttpGet]
    [Route("/notificaciones/getNotificaciones")]
    public async Task<IResult> getNotificaciones(string token)
    {
        var dbDynamic=ObtenerContextoDinamico(token,"PACIENTE")??
        ObtenerContextoDinamico(token,"DOCTOR")??
        ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            int idUser=AuthenticationController.sesiones[token].idUsuario;
            var notificaciones=dbDynamic.Notificaciones.Where(n=>n.IdUsuario==idUser&&n.Leida==false).Select(n=>n.Mensaje).ToList();
            return Results.Ok(notificaciones);
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpPut]
    [Route("/notificaciones/eliminar")]
    public async Task<IResult> eliminarNotificaciones(string token)
    {
        var dbDynamic=ObtenerContextoDinamico(token,"PACIENTE")??
        ObtenerContextoDinamico(token,"DOCTOR")??
        ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            int idUser=AuthenticationController.sesiones[token].idUsuario;
            var notis=dbDynamic.Notificaciones.Where(n=>n.IdUsuario==idUser).ToList();
            foreach(Notificacione not in notis)
            {
                not.Leida=true;
            }
            await dbDynamic.SaveChangesAsync();
            return Results.Ok("Se elimino la notificacion");
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
 }