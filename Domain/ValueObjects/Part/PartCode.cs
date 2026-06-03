using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

// Value Object que encapsula y valida un valor especifico de PartCode.
public readonly record struct PartCode
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartCode(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartCode), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
