// Responsabilidad: Representacion de error reutilizable para validaciones, reglas de negocio y respuestas consistentes.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Application.Common.Errors;

public sealed record Error(string Code, string Message)
{
    public static Error None => new(string.Empty, string.Empty);

    public static Error Validation(string message) => new("Validation", message);

    public static Error NotFound(string message) => new("NotFound", message);

    public static Error Conflict(string message) => new("Conflict", message);

    public static Error Failure(string message) => new("Failure", message);
}
