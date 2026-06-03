using Application.Common.Errors;

namespace Application.Common.Results;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public class Result
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("Un resultado exitoso no debe contener errores.");
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Un resultado fallido debe contener un error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}
