var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddEndpointsApiExplorer();  // Para la exploraci�n de la API (Swagger)
builder.Services.AddSwaggerGen();  // Para generar la documentaci�n de Swagger

var app = builder.Build();

// Crear algunos art�culos en memoria
var articles = new List<string>
{
    "Art�culo 1: Introducci�n a Minimal APIs",
    "Art�culo 2: �Qu� es .NET 9?",
    "Art�culo 3: Creando una API RESTful"
};

// Endpoint GET para obtener todos los art�culos
app.MapGet("/api/articles", () => Results.Ok(articles));

// Endpoint POST para agregar un art�culo
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
