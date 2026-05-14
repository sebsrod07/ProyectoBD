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
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
Dictionary<string, LoginInfo> sesiones = new();


 app.Run();
