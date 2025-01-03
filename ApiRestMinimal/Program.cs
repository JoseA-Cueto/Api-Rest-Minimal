using MiApiMinimal.Data;
using MiApiMinimal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Art�culos", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Art�culos V1");
    });
}

ConfigureApiEndpoints(app);

app.Run();

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

