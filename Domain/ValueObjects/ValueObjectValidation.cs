// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de ValueObjectValidation, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
namespace Domain.ValueObjects;

internal static class ValueObjectValidation
{
    public static string Required(string value, string name, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{name} es obligatorio.", name);
        }

        value = value.Trim();
        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{name} no puede superar {maxLength} caracteres.", name);
        }

        return value;
    }

    public static string? Optional(string? value, string name, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        value = value.Trim();
        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{name} no puede superar {maxLength} caracteres.", name);
        }

        return value;
    }

    public static int Positive(int value, string name)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(name, $"{name} debe ser mayor que cero.");
        }

        return value;
    }

    public static int NonNegative(int value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(name, $"{name} no puede ser negativo.");
        }

        return value;
    }

    public static decimal Money(decimal value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(name, $"{name} no puede ser negativo.");
        }

        return decimal.Round(value, 2);
    }
}
