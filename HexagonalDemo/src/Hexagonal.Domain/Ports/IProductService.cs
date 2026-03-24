namespace Hexagonal.Domain.Ports;
public interface IProductService {
    Task<string> CheckSystemStatus();
}