using Caso2_EmpresaServicioConsultoria.Models;
using Caso2_EmpresaServicioConsultoria.Repository;
using Caso2_EmpresaServicioConsultoria.Repository.Implements;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Servicios


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Configuración de Swagger
builder.Services.AddSwaggerGen();


// Conexión a PostgreSQL (XDbContext)
builder.Services.AddDbContext<ConsultoriaDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Registro de Repositorios (Inyección de Dependencias)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



// Registro del Unit of Work
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// OpenAPI (Estándar de .NET 9)
builder.Services.AddOpenApi();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
  
    // Configuración de Swagger para que cargue al inicio
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Universidad API v1");
        c.RoutePrefix = string.Empty;
    });
}




app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();