using Domain.ValueObjects;

namespace Domain.ValueObjects.Neighborhood;

// Value Object que encapsula y valida un valor especifico de NeighborhoodCityId.
public readonly record struct NeighborhoodCityId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public NeighborhoodCityId(int value) => Value = ValueObjectValidation.Positive(value, nameof(NeighborhoodCityId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de NeighborhoodName.
public readonly record struct NeighborhoodName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public NeighborhoodName(string value) => Value = ValueObjectValidation.Required(value, nameof(NeighborhoodName), 100);
    public string Value { get; }
}
