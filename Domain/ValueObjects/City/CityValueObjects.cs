using Domain.ValueObjects;

namespace Domain.ValueObjects.City;

// Value Object que encapsula y valida un valor especifico de CityDepartmentId.
public readonly record struct CityDepartmentId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public CityDepartmentId(int value) => Value = ValueObjectValidation.Positive(value, nameof(CityDepartmentId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de CityName.
public readonly record struct CityName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public CityName(string value) => Value = ValueObjectValidation.Required(value, nameof(CityName), 100);
    public string Value { get; }
}
