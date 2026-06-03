namespace Application.Common.Exceptions;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed class NotFoundException : Exception
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string entityName, object key)
        : base($"No se encontró {entityName} con identificador '{key}'.")
    {
    }
}
