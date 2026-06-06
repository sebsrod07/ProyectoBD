
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;
public class UsuariosController:BaseController
{
    public UsuariosController(IConfiguration config):base(config)
    {}
    [HttpPost]
    [Route("/crearUsuario")]
    public async Task<IResult> crearUsuario(string idUsuario, string pass, [FromBody]Persona P)
    {
        try
        {
            var dbDynamic=ObtenerContextoDinamico("CREACIONUSER","");
            if(dbDynamic is null)
                throw new Exception("Algo salio mal al conectarse a la BD");
            await dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC crearUsuarioPaciente 
                @nombreUser = {idUsuario}, 
                @contra = {pass}, 
                @pNom = {P.PrimerNombre}, 
                @sNom = {P.SegundoNombre}, 
                @aP = {P.ApellidoPaterno}, 
                @aM = {P.ApellidoMaterno}, 
                @fn = {P.FechaNacimiento.ToString("yyyy-MM-dd")}, 
                @curp = {P.Curp}
        ");
            await dbDynamic.SaveChangesAsync();
            return Results.Ok("CREADO");
        }
        catch(Exception ex)
        {
            string errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return Results.BadRequest($"Error de SQL: {errorReal}");
        } 
        
    }
    
}