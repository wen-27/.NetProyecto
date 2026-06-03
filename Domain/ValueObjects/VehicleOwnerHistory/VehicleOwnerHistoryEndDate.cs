namespace Domain.ValueObjects.VehicleOwnerHistory;

// Value Object que encapsula y valida un valor especifico de VehicleOwnerHistoryEndDate.
public readonly record struct VehicleOwnerHistoryEndDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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
