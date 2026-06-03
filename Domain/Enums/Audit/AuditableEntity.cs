// Responsabilidad: Enumeracion de dominio que nombra estados o categorias permitidas para AuditableEntity, evitando literales magicos.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Enums.Audit;

public enum AuditableEntity
{
    Person = 1,
    Vehicle = 2,
    ServiceOrder = 3,
    Part = 4,
    Invoice = 5,
    User = 6,
    Role = 7
}
