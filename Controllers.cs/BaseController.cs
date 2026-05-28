using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;

public class BaseController : ControllerBase
{
    // Usamos 'protected' para que los controladores hijos puedan acceder a la configuración
    protected readonly IConfiguration _config;

    public BaseController(IConfiguration config)
    {
        _config = config;
    }


    protected ProyectoBdV1Context ObtenerContextoDinamico(string token, string rolRequerido)
    {
        if (!AuthenticationController.sesiones.ContainsKey(token))
            return null;

        string rol = AuthenticationController.sesiones[token].permiso.ToUpper();

        if (rol != rolRequerido.ToUpper())
            return null;

        string configKey = rol switch
        {
            "DOCTOR" => "DoctorConnection",
            "SECRETARIO" => "SecretarioConnection", 
            "PACIENTE" => "PacienteConnection",
            _ => null
        };

        if (configKey == null)
            return null;

        string cs = _config.GetConnectionString(configKey);
        var options = new DbContextOptionsBuilder<ProyectoBdV1Context>().UseSqlServer(cs).Options;
        
        return new ProyectoBdV1Context(options);
    }
}