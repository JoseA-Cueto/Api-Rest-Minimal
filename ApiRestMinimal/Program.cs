using MiApiMinimal.Data;
using MiApiMinimal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

// Logger configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("starting server");

var builder = WebApplication.CreateBuilder(args);
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

var app = builder.Build();
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

   
    app.MapGet("/api/articles/{id}", async (int id, ApplicationDbContext db) =>
    {
        var article = await db.Articles.FindAsync(id);
        return article is not null ? Results.Ok(article) : Results.NotFound();
    });

 
    app.MapPost("/api/articles", async (Article article, ApplicationDbContext db) =>
    {
        db.Articles.Add(article);
        await db.SaveChangesAsync();
        return Results.Created($"/api/articles/{article.Id}", article);
    });


    app.MapPut("/api/articles/{id}", async (int id, Article updatedArticle, ApplicationDbContext db) =>
    {
        var article = await db.Articles.FindAsync(id);
        if (article is null) return Results.NotFound();

        article.Title = updatedArticle.Title;
        article.Content = updatedArticle.Content;
        await db.SaveChangesAsync();

        return Results.Ok(article);
    });


    app.MapDelete("/api/articles/{id}", async (int id, ApplicationDbContext db) =>
    {
        var article = await db.Articles.FindAsync(id);
        if (article is null) return Results.NotFound();

        db.Articles.Remove(article);
        await db.SaveChangesAsync();
        return Results.Ok();
    });
}

