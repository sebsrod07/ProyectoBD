using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;

public class BaseController : ControllerBase
{
    protected readonly IConfiguration _config;

    public BaseController(IConfiguration config)
    {
        _config = config;
    }


    protected ProyectoBdContext ObtenerContextoDinamico(string token, string rolRequerido)
    {
         if(token=="CREACIONUSER")
        {
            string csA = _config.GetConnectionString("altaConnection");
            var optionsA = new DbContextOptionsBuilder<ProyectoBdContext>().UseSqlServer(csA).Options;
            return new ProyectoBdContext(optionsA);
        }
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
            "DEFAULT"=>"DefaultConnection",
            _ => null
        };

        if (configKey == null)
            return null;

        string cs = _config.GetConnectionString(configKey);
        var options = new DbContextOptionsBuilder<ProyectoBdContext>().UseSqlServer(cs).Options;
        
        return new ProyectoBdContext(options);
    }
}