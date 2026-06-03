// Responsabilidad: Enumeracion de dominio que nombra estados o categorias permitidas para UserStatus, evitando literales magicos.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Enums.User;

public enum UserStatus
{
    Inactive = 0,
    Active = 1
}
