using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly ProyectoBdV1Context _db;
    private readonly IConfiguration _config;

    public static Dictionary<string, LoginInfo> sesiones = new();

    public AuthenticationController(ProyectoBdV1Context db, IConfiguration config) : base(config)
    {
        _db = db;
        _config = config;
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IResult> Login(Models.LoginRequest request)
    {
        string cs=string.Empty;
        var user= _db.Usuarios.FirstOrDefault(u=>u.Contraseña==request.password && u.NombreUsuario==request.nombreUsuario);
        if(user is null)
            return Results.NotFound();
        string token =string.Empty;
    
        if(user.Permiso.ToUpper()=="DOCTOR")
        {
            cs = _config.GetConnectionString("DoctorConecction");
            token=Guid.NewGuid().ToString();
            sesiones[token] = new LoginInfo{permiso=user.Permiso,idUsuario=user.IdUsuario};
        }
        else if(user.Permiso.ToUpper()=="PACIENTE")
        {
            cs = _config.GetConnectionString("DoctorConecction");
            token=Guid.NewGuid().ToString();
            sesiones[token] = new LoginInfo{permiso=user.Permiso,idUsuario=user.IdUsuario};
        }
        try
        {
            var options=new DbContextOptionsBuilder<ProyectoBdV1Context>().UseSqlServer(cs).Options;
            var dbDynamic=new ProyectoBdV1Context(options);
            await dbDynamic.Database.OpenConnectionAsync();
            return Results.Ok(new { token=token});
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
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
}