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
app.Run();

//dotnet ef dbcontext scaffold "Data Source=db-project-server.database.windows.net;Initial Catalog=ProyectoBD;Persist Security Info=True;User ID=doctorLogin;Password=ProyectoBD2026_LogDoc;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword" Microsoft.EntityFrameworkCore.Sqlserver -o Models --namespace Models -f