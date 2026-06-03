// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de CityValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.City;

public readonly record struct CityDepartmentId
{
    public CityDepartmentId(int value) => Value = ValueObjectValidation.Positive(value, nameof(CityDepartmentId));
    public int Value { get; }
}

public readonly record struct CityName
{
    public CityName(string value) => Value = ValueObjectValidation.Required(value, nameof(CityName), 100);
    public string Value { get; }
}
