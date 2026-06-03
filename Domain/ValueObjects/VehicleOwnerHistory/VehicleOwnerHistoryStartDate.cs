namespace Domain.ValueObjects.VehicleOwnerHistory;

// Value Object que encapsula y valida un valor especifico de VehicleOwnerHistoryStartDate.
public readonly record struct VehicleOwnerHistoryStartDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public DateTime Value { get; }

    public VehicleOwnerHistoryStartDate(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de inicio de propiedad es obligatoria.", nameof(value));
        }

        if (value > DateTime.UtcNow.AddMinutes(5))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La fecha de inicio de propiedad no puede estar en el futuro.");
        }

        Value = value;
    }

    public override string ToString() => Value.ToString("O");
}
