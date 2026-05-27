using Api.DTOs.VehicleEntryInventory;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.VehicleEntryInventory;

public sealed class VehicleEntryInventoryController : CrudController<Domain.Entities.VehicleEntryInventory, CreateVehicleEntryInventoryRequest, UpdateVehicleEntryInventoryRequest, VehicleEntryInventoryResponse>
{
    public VehicleEntryInventoryController(ISender sender) : base(sender) { }
}
