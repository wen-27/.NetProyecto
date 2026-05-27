namespace Domain.ValueObjects.VehicleOwnerHistory;

public readonly record struct VehicleOwnerHistoryStartDate
{
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
