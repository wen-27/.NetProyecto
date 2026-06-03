using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

// Value Object que encapsula y valida un valor especifico de PartCategoryId.
public readonly record struct PartCategoryId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartCategoryId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PartCategoryId));
    }
    public int Value { get; }
}
