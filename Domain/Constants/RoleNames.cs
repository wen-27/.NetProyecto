namespace Domain.Constants;

// Constantes compartidas para evitar repetir literales importantes del dominio.
public static class RoleNames
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public const string Admin = "Admin";
    public const string Receptionist = "Receptionist";
    public const string Mechanic = "Mechanic";
    public const string Client = "Client";
    public const string WorkshopChief = "WorkshopChief";
    public const string WarehouseChief = "WarehouseChief";
    public const string InventoryManager = "InventoryManager";

    public const string AdminOrReceptionist = Admin + "," + Receptionist;
    public const string AdminOrMechanic = Admin + "," + Mechanic;
    public const string AdminOrWorkshopChief = Admin + "," + WorkshopChief;
    public const string AdminOrWarehouseChief = Admin + "," + WarehouseChief;
    public const string AdminOrInventoryManager = Admin + "," + InventoryManager;
    public const string AnyInternalRole = Admin + "," + Mechanic + "," + Receptionist + "," + WorkshopChief + "," + WarehouseChief + "," + InventoryManager;
}
