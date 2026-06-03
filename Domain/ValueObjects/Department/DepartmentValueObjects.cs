using Domain.ValueObjects;

namespace Domain.ValueObjects.Department;

// Value Object que encapsula y valida un valor especifico de DepartmentCountryId.
public readonly record struct DepartmentCountryId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public DepartmentCountryId(int value) => Value = ValueObjectValidation.Positive(value, nameof(DepartmentCountryId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de DepartmentName.
public readonly record struct DepartmentName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public DepartmentName(string value) => Value = ValueObjectValidation.Required(value, nameof(DepartmentName), 100);
    public string Value { get; }
}
