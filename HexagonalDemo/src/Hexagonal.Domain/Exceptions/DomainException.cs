namespace Hexagonal.Domain.Exceptions;

// Clase base para errores de negocio
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}