using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

// Value Object que encapsula y valida un valor especifico de PartBrandId.
public readonly record struct PartBrandId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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
