using Domain.ValueObjects;

namespace Domain.ValueObjects.Neighborhood;

public readonly record struct NeighborhoodCityId
{
    public NeighborhoodCityId(int value) => Value = ValueObjectValidation.Positive(value, nameof(NeighborhoodCityId));
    public int Value { get; }
}

public readonly record struct NeighborhoodName
{
    public NeighborhoodName(string value) => Value = ValueObjectValidation.Required(value, nameof(NeighborhoodName), 100);
    public string Value { get; }
}
