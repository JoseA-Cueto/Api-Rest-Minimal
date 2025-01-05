using MiApiMinimal.Data;
using MiApiMinimal.Models;
using MiAplicacion.Exceptions;
using MiAplicacion.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using MiApiMinimal.Mappings;

// Logger configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("starting server");

var builder = WebApplication.CreateBuilder(args);
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
   
    app.MapGet("/api/articles", async (ApplicationDbContext db) =>
    {
        var articles = await db.Articles.ToListAsync();
        return Results.Ok(articles);
    });

    
    app.MapGet("/api/articles/{id}", async (Guid id, ApplicationDbContext db) =>
    {
        var article = await db.Articles.FindAsync(id);
        if (article is null)
            throw new NotFoundException("Article not found");

        return Results.Ok(article);
    });

   
    app.MapPost("/api/articles", async (Article article, ApplicationDbContext db) =>
    {
        if (string.IsNullOrWhiteSpace(article.Title) || string.IsNullOrWhiteSpace(article.Content))
        {
            throw new ValidationException("Title and Content", "Cannot be empty.");
        }

       
        article.Id = Guid.NewGuid();

        db.Articles.Add(article);
        await db.SaveChangesAsync();
        return Results.Created($"/api/articles/{article.Id}", article);
    });

 
    app.MapPut("/api/articles/{id}", async (Guid id, Article updatedArticle, ApplicationDbContext db) =>
    {
        if (string.IsNullOrWhiteSpace(updatedArticle.Title) || string.IsNullOrWhiteSpace(updatedArticle.Content))
        {
            throw new ValidationException("Title and Content", "Cannot be empty.");
        }

        var article = await db.Articles.FindAsync(id);
        if (article is null)
            throw new NotFoundException("Article not found");

        article.Title = updatedArticle.Title;
        article.Content = updatedArticle.Content;
        await db.SaveChangesAsync();

        return Results.Ok(article);
    });

  
    app.MapDelete("/api/articles/{id}", async (Guid id, ApplicationDbContext db) =>
    {
        var article = await db.Articles.FindAsync(id);
        if (article is null)
            throw new NotFoundException("Article not found");

        db.Articles.Remove(article);
        await db.SaveChangesAsync();
        return Results.Ok();
    });
}


