using Domain.ValueObjects;

namespace Domain.ValueObjects.StreetType;

// Value Object que encapsula y valida un valor especifico de StreetTypeName.
public readonly record struct StreetTypeName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public StreetTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(StreetTypeName), 50);
    public string Value { get; }
}
