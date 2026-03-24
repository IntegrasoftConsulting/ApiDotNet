namespace Hexagonal.Domain.Exceptions;
public class ProductNotFoundException : DomainException
{
    public ProductNotFoundException(Guid id) 
        : base($"El producto con ID {id} no fue encontrado en el sistema.") { }
}