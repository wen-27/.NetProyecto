// Responsabilidad: Enumeracion de dominio que nombra estados o categorias permitidas para ServiceTypeKind, evitando literales magicos.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Enums.ServiceType;

public enum ServiceTypeKind
{
    PreventiveMaintenance = 1,
    Repair = 2,
    Diagnosis = 3
}
