using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

// Value Object que encapsula y valida un valor especifico de ServiceTypeName.
public readonly record struct ServiceTypeName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public ServiceTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(ServiceTypeName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
