// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de ServiceOrderNewSchemaValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderGeneralDescription
{
    public ServiceOrderGeneralDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderGeneralDescription), 2000);
    public string? Value { get; }
}

public readonly record struct ServiceOrderCancellationReason
{
    public ServiceOrderCancellationReason(string? value) => Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderCancellationReason), 1000);
    public string? Value { get; }
}

public readonly record struct ServiceOrderCancellationDate
{
    public ServiceOrderCancellationDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
