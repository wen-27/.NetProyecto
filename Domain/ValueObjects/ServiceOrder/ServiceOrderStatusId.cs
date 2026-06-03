// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de ServiceOrderStatusId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderStatusId
{
    public ServiceOrderStatusId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderStatusId));
    }
    public int Value { get; }
}
