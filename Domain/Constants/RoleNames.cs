// Responsabilidad: Constantes de dominio compartidas por varias capas para evitar duplicacion de nombres y valores importantes.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
namespace Domain.Constants;

public static class RoleNames
{
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
