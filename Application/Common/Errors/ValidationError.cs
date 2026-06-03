// Responsabilidad: Representacion de error reutilizable para validaciones, reglas de negocio y respuestas consistentes.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Application.Common.Errors;

public sealed record ValidationError(string PropertyName, string ErrorMessage, object? AttemptedValue = null);
