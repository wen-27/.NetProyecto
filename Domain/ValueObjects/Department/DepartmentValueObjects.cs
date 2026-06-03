// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de DepartmentValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Department;

public readonly record struct DepartmentCountryId
{
    public DepartmentCountryId(int value) => Value = ValueObjectValidation.Positive(value, nameof(DepartmentCountryId));
    public int Value { get; }
}

public readonly record struct DepartmentName
{
    public DepartmentName(string value) => Value = ValueObjectValidation.Required(value, nameof(DepartmentName), 100);
    public string Value { get; }
}
