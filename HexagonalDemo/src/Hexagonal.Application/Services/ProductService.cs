namespace Hexagonal.Application.Services;

using Hexagonal.Domain.Ports;

public class ProductService : IProductService {
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) {
        _repository = repository; // IoC vía constructor
    }

    public async Task<string> CheckSystemStatus() => 
        $"Service calling: {await _repository.GetStatusAsync()}";
}