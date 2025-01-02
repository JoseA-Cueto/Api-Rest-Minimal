var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddEndpointsApiExplorer();  // Para la exploración de la API (Swagger)
builder.Services.AddSwaggerGen();  // Para generar la documentación de Swagger

var app = builder.Build();

// Crear algunos artículos en memoria
var articles = new List<string>
{
    "Artículo 1: Introducción a Minimal APIs",
    "Artículo 2: ¿Qué es .NET 9?",
    "Artículo 3: Creando una API RESTful"
};

// Endpoint GET para obtener todos los artículos
app.MapGet("/api/articles", () => Results.Ok(articles));

// Endpoint POST para agregar un artículo
app.MapPost("/api/articles", (string article) =>
{
    articles.Add(article);
    return Results.Created($"/api/articles/{articles.Count - 1}", article);
});

// Configurar Swagger en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
