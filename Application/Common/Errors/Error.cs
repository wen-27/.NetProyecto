namespace Application.Common.Errors;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed record Error(string Code, string Message)
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public static Error None => new(string.Empty, string.Empty);

    public static Error Validation(string message) => new("Validation", message);

    public static Error NotFound(string message) => new("NotFound", message);

    public static Error Conflict(string message) => new("Conflict", message);

    public static Error Failure(string message) => new("Failure", message);
}
