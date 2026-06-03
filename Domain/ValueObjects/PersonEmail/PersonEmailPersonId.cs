using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

// Value Object que encapsula y valida un valor especifico de PersonEmailPersonId.
public readonly record struct PersonEmailPersonId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonEmailPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonEmailPersonId));
    }
    public int Value { get; }
}
