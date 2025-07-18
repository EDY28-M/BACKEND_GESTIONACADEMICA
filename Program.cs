using API_REST_CURSOSACADEMICOS.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using static API_REST_CURSOSACADEMICOS.Data.GestionAcademicaContext;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
var AllowAll = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAll,
    policy =>
    {
        policy.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod();
    });
});

// Configuración de la base de datos
builder.Services.AddDbContext<GestionAcademicaContext>(options =>
 options.UseSqlServer(
 builder.Configuration.GetConnectionString("DefaultConnection")
 ?? Environment.GetEnvironmentVariable("DefaultConnection")
 )
);

// Configuración de controladores y JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar puerto dinámico para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Swagger siempre habilitado
app.UseSwagger();
app.UseSwaggerUI();

// No usar HTTPS redirection en Render
// app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors(AllowAll);

app.UseAuthorization();

app.MapControllers();

app.Run();