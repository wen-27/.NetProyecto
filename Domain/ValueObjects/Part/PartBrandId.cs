using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartBrandId
{
    public PartBrandId(int? value)
    {
        if (value is not null && value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El identificador de la marca del repuesto debe ser mayor que cero.");
        }

        Value = value;
    }

    public int? Value { get; }
}
