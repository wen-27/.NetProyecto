// Responsabilidad: Enumeracion de dominio que nombra estados o categorias permitidas para StockStatus, evitando literales magicos.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Enums.Part;

public enum StockStatus
{
    OutOfStock = 1,
    LowStock = 2,
    Available = 3
}
