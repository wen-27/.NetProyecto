// Responsabilidad: Enumeracion de dominio que nombra estados o categorias permitidas para SystemRole, evitando literales magicos.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Enums.Role;

public enum SystemRole
{
    Admin = 1,
    Client = 2,
    Mechanic = 3,
    Receptionist = 4,
    WorkshopChief = 5,
    WarehouseChief = 6,
    InventoryManager = 7
}
