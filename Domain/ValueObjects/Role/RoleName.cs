using Domain.ValueObjects;

namespace Domain.ValueObjects.Role;

// Value Object que encapsula y valida un valor especifico de RoleName.
public readonly record struct RoleName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public RoleName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(RoleName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
