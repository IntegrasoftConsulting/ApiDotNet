namespace Hexagonal.Application.Services;

using Hexagonal.Domain.Ports;
using Hexagonal.Domain.Exceptions;
using Microsoft.Extensions.Logging;

public class ProductService : IProductService {
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ILogger<ProductService> logger) {
        _repository = repository; 
        _logger = logger;
    }

    public async Task<string> CheckSystemStatus() 
    {
        _logger.LogInformation("Iniciando verificación de estado del sistema...");
        // Simulamos una validación de negocio que falla
        var someCondition = false; 
        if (someCondition) 
        {
            throw new ProductNotFoundException(Guid.NewGuid());
        }
        var result = await _repository.GetStatusAsync();
        _logger.LogWarning("El repositorio respondió con éxito: {Result}", result);
        return result;
    }
}