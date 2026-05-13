using System.Reflection.Metadata.Ecma335;
using Azure.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ProyectoBdV1Context>(options =>
   options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")
   ));

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
Dictionary<string, LoginInfo> sesiones = new();


app.MapPost("/login", async(Models.LoginRequest request, ProyectoBdV1Context db ) =>
{
    string cs=string.Empty;
    var user= db.Usuarios.FirstOrDefault(u=>u.Contraseña==request.password && u.NombreUsuario==request.nombreUsuario);
    if(user is null)
        return Results.NotFound();
    string token =string.Empty, tokenid=string.Empty;
    
    if(user.Permiso.ToUpper()=="DOCTOR")
    {
        cs=builder.Configuration.GetConnectionString("DoctorConecction");
        token=Guid.NewGuid().ToString();
        sesiones[token] = new LoginInfo{
            permiso=user.Permiso,
            idUsuario=user.IdUsuario
        };
    }
    try
    {
        var options=new DbContextOptionsBuilder<ProyectoBdV1Context>().UseSqlServer(cs).Options;
        var dbDynamic=new ProyectoBdV1Context(options);
        await dbDynamic.Database.OpenConnectionAsync();
        return Results.Redirect($"/pacientes?token={token}");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }  
}
);
app.MapGet("/pacientes", async (string token) =>
{
    if(!sesiones.ContainsKey(token))
        return Results.Unauthorized();
    var cs=string.Empty;
    string rol=sesiones[token].permiso;
    int idUsuario=sesiones[token].idUsuario;
    if(rol.ToUpper()=="DOCTOR")
    {
        cs=builder.Configuration.GetConnectionString("DoctorConecction");
    }
    else
    {
        return Results.Unauthorized();
    }
    var options =
        new DbContextOptionsBuilder<ProyectoBdV1Context>()
        .UseSqlServer(cs)
        .Options;
        using var dbDynamic =
        new ProyectoBdV1Context(options);

    try
    {
       int idDoctor = await dbDynamic.Database.SqlQuery<int>($"SELECT dbo.getIdDoctor({idUsuario}) as value").FirstAsync();
       var citas =await dbDynamic.Database.SqlQuery<VerCitasResult>($"SELECT * FROM VerCitas({idDoctor})").ToListAsync();
       return Results.Ok(citas);

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


 app.Run();
