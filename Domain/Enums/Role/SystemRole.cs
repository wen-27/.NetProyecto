namespace Domain.Enums.Role;

// Enumeracion que limita los valores permitidos para SystemRole.
public enum SystemRole
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Admin = 1,
    Client = 2,
    Mechanic = 3,
    Receptionist = 4,
    WorkshopChief = 5,
    WarehouseChief = 6,
    InventoryManager = 7
}
