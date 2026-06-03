// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleOwnerHistoryEndDate, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
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
