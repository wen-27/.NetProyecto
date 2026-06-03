namespace Domain.Enums.VehicleOwnerHistory;

// Enumeracion que limita los valores permitidos para OwnershipStatus.
public enum OwnershipStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Historical = 0,
    Current = 1
}
