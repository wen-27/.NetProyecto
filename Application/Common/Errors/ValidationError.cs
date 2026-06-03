namespace Application.Common.Errors;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed record ValidationError(string PropertyName, string ErrorMessage, object? AttemptedValue = null);
