// Responsabilidad: Excepcion de aplicacion usada para expresar errores esperados del negocio sin acoplarlos a detalles HTTP.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Application.Common.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string entityName, object key)
        : base($"No se encontró {entityName} con identificador '{key}'.")
    {
    }
}
