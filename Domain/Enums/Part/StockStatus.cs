namespace Domain.Enums.Part;

// Enumeracion que limita los valores permitidos para StockStatus.
public enum StockStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    OutOfStock = 1,
    LowStock = 2,
    Available = 3
}
