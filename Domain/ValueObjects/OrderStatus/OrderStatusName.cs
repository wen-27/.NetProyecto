// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de OrderStatusName, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderStatus;

public readonly record struct OrderStatusName
{
    public OrderStatusName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(OrderStatusName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
