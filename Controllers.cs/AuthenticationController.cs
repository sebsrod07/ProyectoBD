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
    // 1. EL TRY DEBE INICIAR DESDE EL MILISEGUNDO CERO
    try
    {
        string cs = string.Empty;
        
        // ¡Si esto falla, ahora sí caerá en el catch de abajo!
        var user = _db.Usuarios.FirstOrDefault(u => u.Contraseña == request.password && u.NombreUsuario == request.nombreUsuario);
        
        if (user is null)
            return Results.NotFound();
            
        string token = string.Empty;

        if (user.Permiso.ToUpper() == "DOCTOR")
        {
            cs = _config.GetConnectionString("DoctorConnection");
            token = Guid.NewGuid().ToString();
            sesiones[token] = new LoginInfo { permiso = user.Permiso, idUsuario = user.IdUsuario };
        }
        else if (user.Permiso.ToUpper() == "PACIENTE")
        {
            cs = _config.GetConnectionString("PacienteConnection");
            // OJO: Borré la línea duplicada que sobreescribía con "DoctorConnection"
            token = Guid.NewGuid().ToString();
            sesiones[token] = new LoginInfo { permiso = user.Permiso, idUsuario = user.IdUsuario };
        }

        var options = new DbContextOptionsBuilder<ProyectoBdContext>().UseSqlServer(cs).Options;
        var dbDynamic = new ProyectoBdContext(options);
        await dbDynamic.Database.OpenConnectionAsync();
        
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