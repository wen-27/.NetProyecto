using Domain.ValueObjects;

namespace Domain.ValueObjects.PartCategory;

// Value Object que encapsula y valida un valor especifico de PartCategoryName.
public readonly record struct PartCategoryName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartCategoryName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartCategoryName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
