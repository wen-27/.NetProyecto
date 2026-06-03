// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleOwnerHistoryVehicleId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleOwnerHistory;

public readonly record struct VehicleOwnerHistoryVehicleId
{
    public VehicleOwnerHistoryVehicleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleOwnerHistoryVehicleId));
    }
    public int Value { get; }
}
