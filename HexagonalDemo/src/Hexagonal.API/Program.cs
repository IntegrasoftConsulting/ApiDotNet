using Hexagonal.Application.Services;
using Hexagonal.Domain.Ports;
using Hexagonal.Infrastructure.Persistence;
using Hexagonal.API.Handlers;

var builder = WebApplication.CreateBuilder(args);

// 1. Agrego inyección de dependencias IoC
builder.Services.AddScoped<IProductRepository, MySqlRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// 2. Registro el Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // Agrega metadatos estándar a los errores

// 3. Agrego documentación con Swagger
builder.Services.AddEndpointsApiExplorer();  // <-- aquí
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Activo Middleware (Debe ir antes de los endpoints)
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Configura la interfaz gráfica
    app.UseSwaggerUI(options =>
    {
        // Define dónde está el JSON que acabamos de generar
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hexagonal API V1");
        
        // OPTIMIZACIÓN: Swagger carga en la raíz (ej: https://...-5000.app.github.dev/)
        options.RoutePrefix = string.Empty; 
        
        // Opcional: Muestra el tiempo de respuesta en la UI
        options.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

// Prueba de error para verificar que funciona
app.MapGet("/error-test", () => { throw new Exception("¡Bomba en la infraestructura!"); });

app.MapGet("/status", async (IProductService service) =>
{
    var result = await service.CheckSystemStatus();
    return Results.Ok(new { Status = result });
})
.WithName("GetStatus");

app.Run();