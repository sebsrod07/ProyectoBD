using Microsoft.EntityFrameworkCore;
using Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ProyectoBdContext>(options =>
   options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
   ));
builder.Services.AddEndpointsApiExplorer();

 var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
Dictionary<string, LoginInfo> sesiones = new();
app.MapGet("/",() =>"HOLA");
app.Run();
