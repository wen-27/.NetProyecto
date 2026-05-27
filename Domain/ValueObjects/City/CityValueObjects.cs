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
