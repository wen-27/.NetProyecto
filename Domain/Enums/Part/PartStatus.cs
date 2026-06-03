namespace Domain.Enums.Part;

// Enumeracion que limita los valores permitidos para PartStatus.
public enum PartStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Inactive = 0,
    Active = 1
}
