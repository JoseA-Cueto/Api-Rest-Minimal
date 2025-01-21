using System.Reflection;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

// Logger configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("starting server");

var builder = WebApplication.CreateBuilder(args);
{
    // Registrar dependencias usando el contenedor
    builder.Services.AddApplicationServices();
    
    // Agregar Validators
    builder.Services.AddValidatorsFromAssemblyContaining<Program>();
    
    // Agregar AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    
    // Logger connection
    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });
    
    // Database connection
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
    builder.Services.AddScoped<ApiRestMinimal.Custom.Utility>();

    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))

        };

    } );
    // Other services
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Art�culos", Version = "v1" });
        });

    // Cors
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Art�culos V1"); });
    }

    app.UseCors("AllowAll");
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseSerilogRequestLogging();
    app.UseAuthentication();
  
    app.UseRouting();
    
    app.MapArticleEndpoints();
    app.MapImageEndpoints();
    app.MapUserEndpoints();

    app.Run();
}