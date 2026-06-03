// Responsabilidad: Enumeracion de dominio que nombra estados o categorias permitidas para DocumentTypeKind, evitando literales magicos.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Enums.DocumentType;

public enum DocumentTypeKind
{
    CitizenshipCard = 1,
    IdentityCard = 2,
    Passport = 3,
    ForeignId = 4,
    TaxId = 5,
    DriverLicense = 6
}
