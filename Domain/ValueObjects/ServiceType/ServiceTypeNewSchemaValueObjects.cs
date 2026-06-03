// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de ServiceTypeNewSchemaValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

public readonly record struct ServiceTypeEstimatedDays
{
    public ServiceTypeEstimatedDays(int value) => Value = ValueObjectValidation.Positive(value, nameof(ServiceTypeEstimatedDays));
    public int Value { get; }
}
