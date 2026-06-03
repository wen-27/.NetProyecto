namespace Application.Common.Exceptions;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed class ConflictException : Exception
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public ConflictException(string message)
        : base(message)
    {
    }
}
