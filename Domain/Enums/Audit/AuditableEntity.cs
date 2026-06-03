namespace Domain.Enums.Audit;

// Enumeracion que limita los valores permitidos para AuditableEntity.
public enum AuditableEntity
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Person = 1,
    Vehicle = 2,
    ServiceOrder = 3,
    Part = 4,
    Invoice = 5,
    User = 6,
    Role = 7
}
