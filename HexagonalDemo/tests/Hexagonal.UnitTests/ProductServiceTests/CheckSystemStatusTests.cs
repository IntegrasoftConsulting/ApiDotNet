using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Hexagonal.Application.Services;
using Hexagonal.Domain.Ports;

namespace Hexagonal.UnitTests.ProductServiceTests;

public class CheckSystemStatusTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<ILogger<ProductService>> _loggerMock;
    private readonly ProductService _service;

    public CheckSystemStatusTests()
    {
        // Creamos los "dobles" de las interfaces
        _repositoryMock = new Mock<IProductRepository>();
        _loggerMock = new Mock<ILogger<ProductService>>();

        // Inyectamos los mocks al servicio (IoC manual para el test)
        _service = new ProductService(_repositoryMock.Object, _loggerMock.Object);
        
    }

    [Fact]
    public async Task CheckSystemStatus_ShouldReturnStatusFromRepository()
    {
        // ARRANGE (Preparar)
        var expectedStatus = "Conexión Exitosa con DB";
        _repositoryMock.Setup(repo => repo.GetStatusAsync())
                       .ReturnsAsync(expectedStatus);

        // ACT (Actuar)
        var result = await _service.CheckSystemStatus();

        // ASSERT (Verificar)
        Assert.Equal(expectedStatus, result);
        
        // Verificamos que el logger fue llamado al menos una vez
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
            Times.AtLeastOnce);
    }
}