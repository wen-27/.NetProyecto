// Responsabilidad: Modelo de resultado usado para transportar exito, fallo y datos sin lanzar excepciones para flujos esperados.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Application.Common.Errors;

namespace Application.Common.Results;

public sealed class Result<T> : Result
{
    private readonly T? _value;

    private Result(T value)
        : base(true, Error.None)
    {
        _value = value;
    }

    private Result(Error error)
        : base(false, error)
    {
    }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("No se puede acceder al valor de un resultado fallido.");

    public static Result<T> Success(T value) => new(value);

    public static new Result<T> Failure(Error error) => new(error);
}
