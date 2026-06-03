using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

// Value Object que encapsula y valida un valor especifico de PartStock.
public readonly record struct PartStock
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartStock(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(PartStock));
    }
    public int Value { get; }
}
