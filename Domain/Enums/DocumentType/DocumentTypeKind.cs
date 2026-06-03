namespace Domain.Enums.DocumentType;

// Enumeracion que limita los valores permitidos para DocumentTypeKind.
public enum DocumentTypeKind
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    CitizenshipCard = 1,
    IdentityCard = 2,
    Passport = 3,
    ForeignId = 4,
    TaxId = 5,
    DriverLicense = 6
}
