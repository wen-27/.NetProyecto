namespace Domain.Enums.ServiceType;

// Enumeracion que limita los valores permitidos para ServiceTypeKind.
public enum ServiceTypeKind
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    PreventiveMaintenance = 1,
    Repair = 2,
    Diagnosis = 3
}
