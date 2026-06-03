// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleEntryInventoryValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleEntryInventory;

public readonly record struct VehicleEntryInventoryServiceOrderId
{
    public VehicleEntryInventoryServiceOrderId(int value) => Value = ValueObjectValidation.Positive(value, nameof(VehicleEntryInventoryServiceOrderId));
    public int Value { get; }
}

public readonly record struct VehicleEntryInventoryHasScratches
{
    public VehicleEntryInventoryHasScratches(bool value) => Value = value;
    public bool Value { get; }
}

public readonly record struct VehicleEntryInventoryScratchesDescription
{
    public VehicleEntryInventoryScratchesDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(VehicleEntryInventoryScratchesDescription), 1000);
    public string? Value { get; }
}

public readonly record struct VehicleEntryInventoryHasToolbox
{
    public VehicleEntryInventoryHasToolbox(bool value) => Value = value;
    public bool Value { get; }
}

public readonly record struct VehicleEntryInventoryToolboxDescription
{
    public VehicleEntryInventoryToolboxDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(VehicleEntryInventoryToolboxDescription), 1000);
    public string? Value { get; }
}

public readonly record struct VehicleEntryInventoryOwnershipCardDelivered
{
    public VehicleEntryInventoryOwnershipCardDelivered(bool value) => Value = value;
    public bool Value { get; }
}

public readonly record struct VehicleEntryInventoryObservations
{
    public VehicleEntryInventoryObservations(string? value) => Value = ValueObjectValidation.Optional(value, nameof(VehicleEntryInventoryObservations), 1000);
    public string? Value { get; }
}

public readonly record struct VehicleEntryInventoryRegisteredAt
{
    public VehicleEntryInventoryRegisteredAt(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de registro es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}
