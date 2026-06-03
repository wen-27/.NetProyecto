using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

// Value Object que encapsula y valida un valor especifico de PartDescription.
public readonly record struct PartDescription
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartDescription(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartDescription), 255);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
