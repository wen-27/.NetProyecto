using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

// Value Object que encapsula y valida un valor especifico de PartMinimumStock.
public readonly record struct PartMinimumStock
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartMinimumStock(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(PartMinimumStock));
    }
    public int Value { get; }
}
