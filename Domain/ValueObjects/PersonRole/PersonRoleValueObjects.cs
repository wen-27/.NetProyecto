using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonRole;

// Value Object que encapsula y valida un valor especifico de PersonRolePersonId.
public readonly record struct PersonRolePersonId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonRolePersonId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PersonRolePersonId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonRoleRoleId.
public readonly record struct PersonRoleRoleId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonRoleRoleId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PersonRoleRoleId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonRoleIsActive.
public readonly record struct PersonRoleIsActive
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonRoleIsActive(bool value) => Value = value;
    public bool Value { get; }
}
