using Hexagonal.Application.Services;
using Hexagonal.Domain.Ports;
using Hexagonal.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, MySqlRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddEndpointsApiExplorer();  // <-- aquí
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

app.MapGet("/status", async (IProductService service) =>
{
    var result = await service.CheckSystemStatus();
    return Results.Ok(new { Status = result });
})
.WithName("GetStatus");

app.Run();