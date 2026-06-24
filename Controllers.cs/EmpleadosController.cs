using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;
namespace Models;
public class EmpleadosController:BaseController
{
    public EmpleadosController(IConfiguration confi): base(confi)
    {}
    [HttpGet]
    [Route("/empleados/getIdEmpleado")]
    public async Task<IResult> getIdEmpleado(string token)
    {
        var dbDynamic=ObtenerContextoDinamico(token, "SECRETARIO")??ObtenerContextoDinamico(token, "DOCTOR");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            var idEmpleado=await dbDynamic.Database.SqlQuery<int>($"SELECT dbo.getIdEmpleado({AuthenticationController.sesiones[token].idUsuario}) as Value").FirstAsync();
            return Results.Ok(idEmpleado);
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpPut]
    [Route("/empleados/setDoctor")]
    public async Task<IResult> setDoctor(string token, [FromBody] SetDoctor? doctorInfo)
    {
        var dbDynamic = ObtenerContextoDinamico(token, "SECRETARIO");
        if (dbDynamic is null)
            return Results.Unauthorized();
            
        try
        {
            if(doctorInfo is null)
            {
                throw new Exception("Ha ocurrido un error, no se úede setear");
            }
            
            await dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"EXEC setDoctor
                @horaInicio = {doctorInfo.DatosEmpleado.HoraInicio},
                @horaFin = {doctorInfo.DatosEmpleado.HoraFin},
                @idEmpleado = {doctorInfo.DatosEmpleado.IdEmpleado},
                @salario = {doctorInfo.DatosEmpleado.Salario},
                @idEspecialidad = {doctorInfo.IdEspecialidad}");
                
            await dbDynamic.SaveChangesAsync();
            return Results.Ok("ACTUALIZADO");
            
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("/empleados/setEmpleado")]
    public async Task<IResult> setEmpleado(string token, [FromBody] SetEmpleado? empleado)
    {
        var dbDynamic = ObtenerContextoDinamico(token, "SECRETARIO");
        if (dbDynamic is null)
            return Results.Unauthorized();
            
        try
        {
            if(empleado is null)
            {
                throw new Exception("Ha ocurrido un error, no se úede setear");
            }
            
            await dbDynamic.Database.ExecuteSqlInterpolatedAsync($@"EXEC setEmpleado
                @horaInicio = {empleado.HoraInicio},
                @horaFin = {empleado.HoraFin},
                @idEmpleado = {empleado.IdEmpleado},
                @salario = {empleado.Salario}");
                
            await dbDynamic.SaveChangesAsync();
            return Results.Ok("ACTUALIZADO");
            
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("/empleados/getEmpleados")]
    public async Task <IResult> getEmpleados(string token, int? idEmpleado)
    {
        var dbDynamic = ObtenerContextoDinamico(token, "SECRETARIO");
        if (dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            var query=dbDynamic.VerNombresEmpleados.AsQueryable();
            if(idEmpleado.HasValue)
                query=query.Where(e=>e.IdEmpleado==idEmpleado);
            query=query.Where(e=>e.Estatus=="Trabajando");
            query=query.OrderBy(c=>c.NombreCompleto);
            return Results.Ok(query.ToList());
        }
        catch(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpPut]
    [Route("/empleados/darDeBaja")]
    public async Task<IResult> darDeBajaEmpleado(string token, int idEmpleado)
    {
        var dbDynamic =ObtenerContextoDinamico(token, "SECRETARIO");
        if(dbDynamic is null)
            return Results.Unauthorized();
        try
        {
            var empleado= await dbDynamic.Empleados.Include(e=>e.Doctores).ThenInclude(c=>c.Cita).FirstOrDefaultAsync(e => e.IdEmpleado == idEmpleado);
            if( empleado is null)
                return Results.NotFound("Empleado no encontrado");
            bool esDoctor=empleado.Doctores is not null && empleado.Doctores.Any();

            if(esDoctor)
            {
                if(empleado.Doctores.SelectMany(d=>d.Cita).Any(c=>c.Estatus=="PENDIENTE DE ATENCION"))
                    throw new Exception("El Doctor tiene citas pendientes");
                else
                    empleado.Estatus="Baja";
            }
            else
                empleado.Estatus="Baja";
            await dbDynamic.SaveChangesAsync();
            return Results.Ok("Empleado dado de baja");
        }
        catch(Exception ex)
        {
            var mensaje=ex.InnerException != null ? ex.InnerException.Message:ex.Message;
            return Results.BadRequest(mensaje);
        }
    }
}