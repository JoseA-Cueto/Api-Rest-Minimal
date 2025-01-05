using MiApiMinimal.Data;
using MiApiMinimal.Models;
using MiAplicacion.Exceptions;
using MiAplicacion.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using MiApiMinimal.Mappings;
using ApiRestMinimal.DTOs;
using AutoMapper;
using ApiRestMinimal.Extensions;

// Logger configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("starting server");

var builder = WebApplication.CreateBuilder(args);

// Registrar dependencias usando el contenedor
builder.Services.AddApplicationServices();

// Agregar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
{
    // Logger connection
    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });
    
    // Database connection
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
    // Other services
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Art�culos", Version = "v1" });
        });
}
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
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionHandlingMiddleware>();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Art�culos V1");
        });
    }
    app.UseSerilogRequestLogging();
    ConfigureApiEndpoints(app);
    app.Run();
}

// TODO: modificar la estructura de los minimal endpoints

void ConfigureApiEndpoints(WebApplication app)
{
    // GET all articles
    app.MapGet("/api/articles", async (ApplicationDbContext db, IMapper mapper) =>
    {
        var articles = await db.Articles.ToListAsync();
        var articleDtos = mapper.Map<List<ArticleDTOs>>(articles);
        return Results.Ok(articleDtos);
    });

    // GET an article by ID
    app.MapGet("/api/articles/{id}", async (Guid id, ApplicationDbContext db, IMapper mapper) =>
    {
        var article = await db.Articles.FindAsync(id);
        if (article is null)
            return Results.NotFound(new { Message = "Article not found" });

        var articleDto = mapper.Map<ArticleDTOs>(article);
        return Results.Ok(articleDto);
    });

    // POST a new article
    app.MapPost("/api/articles", async (ArticleDTOs articleDto, ApplicationDbContext db, IMapper mapper) =>
    {
        if (string.IsNullOrWhiteSpace(articleDto.Title) || string.IsNullOrWhiteSpace(articleDto.Content))
        {
            return Results.BadRequest(new { Message = "Title and Content cannot be empty." });
        }

        var article = mapper.Map<Article>(articleDto);
        article.Id = Guid.NewGuid(); // Generate a new Guid for the article

        db.Articles.Add(article);
        await db.SaveChangesAsync();
        return Results.Created($"/api/articles/{article.Id}", article);
    });

    // PUT (update) an article
    app.MapPut("/api/articles/{id}", async (Guid id, ArticleDTOs updatedArticleDto, ApplicationDbContext db, IMapper mapper) =>
    {
        if (string.IsNullOrWhiteSpace(updatedArticleDto.Title) || string.IsNullOrWhiteSpace(updatedArticleDto.Content))
        {
            return Results.BadRequest(new { Message = "Title and Content cannot be empty." });
        }

        var article = await db.Articles.FindAsync(id);
        if (article is null)
            return Results.NotFound(new { Message = "Article not found" });

        // Map changes from DTO to entity
        mapper.Map(updatedArticleDto, article);
        await db.SaveChangesAsync();

        return Results.Ok(article);
    });

    // DELETE an article
    app.MapDelete("/api/articles/{id}", async (Guid id, ApplicationDbContext db) =>
    {
        var article = await db.Articles.FindAsync(id);
        if (article is null)
            return Results.NotFound(new { Message = "Article not found" });

        db.Articles.Remove(article);
        await db.SaveChangesAsync();
        return Results.Ok(new { Message = "Article deleted successfully" });
    });
}



