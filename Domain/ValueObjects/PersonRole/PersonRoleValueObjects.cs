// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonRoleValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonRole;

public readonly record struct PersonRolePersonId
{
    public PersonRolePersonId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PersonRolePersonId));
    public int Value { get; }
}

public readonly record struct PersonRoleRoleId
{
    public PersonRoleRoleId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PersonRoleRoleId));
    public int Value { get; }
}

public readonly record struct PersonRoleIsActive
{
    public PersonRoleIsActive(bool value) => Value = value;
    public bool Value { get; }
}
