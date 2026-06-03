using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleEntryInventory;

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryServiceOrderId.
public readonly record struct VehicleEntryInventoryServiceOrderId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryServiceOrderId(int value) => Value = ValueObjectValidation.Positive(value, nameof(VehicleEntryInventoryServiceOrderId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryHasScratches.
public readonly record struct VehicleEntryInventoryHasScratches
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryHasScratches(bool value) => Value = value;
    public bool Value { get; }
}

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryScratchesDescription.
public readonly record struct VehicleEntryInventoryScratchesDescription
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryScratchesDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(VehicleEntryInventoryScratchesDescription), 1000);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryHasToolbox.
public readonly record struct VehicleEntryInventoryHasToolbox
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryHasToolbox(bool value) => Value = value;
    public bool Value { get; }
}

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryToolboxDescription.
public readonly record struct VehicleEntryInventoryToolboxDescription
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryToolboxDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(VehicleEntryInventoryToolboxDescription), 1000);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryOwnershipCardDelivered.
public readonly record struct VehicleEntryInventoryOwnershipCardDelivered
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryOwnershipCardDelivered(bool value) => Value = value;
    public bool Value { get; }
}

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryObservations.
public readonly record struct VehicleEntryInventoryObservations
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryObservations(string? value) => Value = ValueObjectValidation.Optional(value, nameof(VehicleEntryInventoryObservations), 1000);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de VehicleEntryInventoryRegisteredAt.
public readonly record struct VehicleEntryInventoryRegisteredAt
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleEntryInventoryRegisteredAt(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de registro es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}
