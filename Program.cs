using Microsoft.EntityFrameworkCore;
using Models;
var builder = WebApplication.CreateBuilder(args);
// --- INICIO DE LA PRUEBA DE FUEGO ---
var cadenaPrueba = builder.Configuration.GetConnectionString("DefaultConnection");
// --- FIN DE LA PRUEBA DE FUEGO ---
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ProyectoBdV1Context>(options =>
   options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
   ));
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
Dictionary<string, LoginInfo> sesiones = new();

app.Run();
