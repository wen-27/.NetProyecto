using Api.DTOs.VehicleEntryInventory;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.VehicleEntryInventory;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con VehicleEntryInventory.
public sealed class VehicleEntryInventoryController : CrudController<Domain.Entities.VehicleEntryInventory, CreateVehicleEntryInventoryRequest, UpdateVehicleEntryInventoryRequest, VehicleEntryInventoryResponse>
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public VehicleEntryInventoryController(ISender sender) : base(sender) { }
}
