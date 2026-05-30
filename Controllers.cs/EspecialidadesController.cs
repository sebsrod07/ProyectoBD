using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.ObjectPool;
namespace Controllers;
[ApiController]
public class EspecialidadController:BaseController
{
    public EspecialidadController(IConfiguration config) :base(config)
    {}
    [HttpGet]
    [Route("/getEspecialidades")]
    public async Task<IResult> verEspecialidades(string token, int? idEspecialidad)
    {
        using var dbDynamic=ObtenerContextoDinamico(token, "PACIENTE");
        if(dbDynamic is null)
            return Results.Unauthorized();
        var especialidades=await dbDynamic.Especialidadads.ToListAsync();
        return Results.Ok(especialidades);

        
    }
    
}