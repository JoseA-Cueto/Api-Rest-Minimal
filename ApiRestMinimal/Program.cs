using System.Text;
using ApiRestMinimal.Common.Extensions;
using ApiRestMinimal.Common.Middleware;
using ApiRestMinimal.Custom;
using ApiRestMinimal.Data;
using ApiRestMinimal.Endpoints.Articles;
using ApiRestMinimal.Endpoints.ImageFiles;
using ApiRestMinimal.Endpoints.Users;
using ApiRestMinimal.Mappings;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
// Configurar loggers
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("Starting server");

// Registro de servicios en contenedor DI
builder.Services.AddApplicationServices();

// Agregar Validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Agregar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Logger de configuración
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

// Agregar configuración de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios personalizados
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<ApiRestMinimal.Custom.Utility>();

// Agregar configuración de JWT
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };
});

// Otros servicios necesarios (Swagger, CORS, etc.)
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Artículos", Version = "v1" });
    });

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configuración de entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Artículos V1"); });
}

// Configuración de middlewares
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseAuthentication();

app.UseRouting();

// Mapear los endpoints
app.MapArticleEndpoints();
app.MapImageEndpoints();
app.MapUserEndpoints();

// Ejecutar la aplicación
app.Run();
