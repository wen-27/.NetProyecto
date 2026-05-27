namespace Domain.ValueObjects.VehicleOwnerHistory;

public readonly record struct VehicleOwnerHistoryEndDate
{
    public DateTime? Value { get; }

    public VehicleOwnerHistoryEndDate(DateTime? value)
    {
        if (value == default(DateTime))
        {
            throw new ArgumentException("La fecha de fin de propiedad no puede ser la fecha predeterminada.", nameof(value));
        }

        Value = value;
    }

    public bool IsCurrent => !Value.HasValue;

    public override string ToString() => Value?.ToString("O") ?? "Actual";
}
