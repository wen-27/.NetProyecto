namespace Application.Common.Exceptions;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed class UnauthorizedException : Exception
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}
