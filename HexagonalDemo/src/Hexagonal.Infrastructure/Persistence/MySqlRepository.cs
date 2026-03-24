namespace Hexagonal.Infrastructure.Persistence;

using Hexagonal.Domain.Ports;

public class MySqlRepository : IProductRepository {
    public Task<string> GetStatusAsync() => Task.FromResult("Data from MySQL");
}