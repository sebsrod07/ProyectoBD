using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly ProyectoBdContext _db;
    private readonly IConfiguration _config;

    public static Dictionary<string, LoginInfo> sesiones = new();

    public AuthenticationController(ProyectoBdContext db, IConfiguration config) : base(config)
    {
        _db = db;
        _config = config;
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IResult> Login(Models.LoginRequest request)
    {

        try
        {
            string cs = string.Empty;
            

            var user = _db.Usuarios.FirstOrDefault(u => u.Contraseña == request.password && u.NombreUsuario == request.nombreUsuario);
            
            if (user is null)
                return Results.NotFound();
            string token = string.Empty;
            var dbDynamic = ObtenerContextoDinamico(token,"PACIENTE")??ObtenerContextoDinamico(token,"DOCTOR")??ObtenerContextoDinamico(token,"SECRETARIO");
            token=Guid.NewGuid().ToString();
            sesiones[token]=new LoginInfo
            {
                permiso=user.Permiso,
                idUsuario=user.IdUsuario
            };
            return Results.Ok(new { token = token });
        }
        catch (Exception ex)
        {
            // 2. USAR RESULTS.PROBLEM PARA IMPRIMIR EL ERROR DETALLADO
            return Results.Problem(detail: ex.ToString(), title: "Error fatal en la base de datos");
        }  
    }
    [HttpGet]
    [Route("/login/permisos")]
    public async Task<IResult> getPermisos(string token)
    {
        if(!sesiones.ContainsKey(token))
            return Results.Unauthorized();
        return Results.Ok(sesiones[token].permiso.ToUpper());
    }
    [HttpGet]
    [Route("/getMiId")]
    public async Task<IResult> getMiId(string token)
    {

        using var dbDynamic=ObtenerContextoDinamico(token,"DEFAULT");
        if(dbDynamic is null)
            return Results.Unauthorized();
        return Results.Ok(sesiones[token].idUsuario);
    }
}