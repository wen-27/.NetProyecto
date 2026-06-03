using Application.Common.Errors;

namespace Application.Common.Results;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed class Result<T> : Result
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
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
