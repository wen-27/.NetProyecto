// Responsabilidad: Enumeracion de dominio que nombra estados o categorias permitidas para OwnershipStatus, evitando literales magicos.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Enums.VehicleOwnerHistory;

public enum OwnershipStatus
{
    Historical = 0,
    Current = 1
}
