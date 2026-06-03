using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

// Value Object que encapsula y valida un valor especifico de PersonEmailDomainId.
public readonly record struct PersonEmailDomainId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonEmailDomainId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonEmailDomainId));
    }
    public int Value { get; }
}
