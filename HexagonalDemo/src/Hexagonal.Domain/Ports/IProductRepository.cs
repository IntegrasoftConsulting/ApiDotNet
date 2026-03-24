namespace Hexagonal.Domain.Ports;
public interface IProductRepository {
    Task<string> GetStatusAsync();
}