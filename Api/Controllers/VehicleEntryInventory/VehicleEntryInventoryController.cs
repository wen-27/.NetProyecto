// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con VehicleEntryInventory. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.DTOs.VehicleEntryInventory;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.VehicleEntryInventory;

public sealed class VehicleEntryInventoryController : CrudController<Domain.Entities.VehicleEntryInventory, CreateVehicleEntryInventoryRequest, UpdateVehicleEntryInventoryRequest, VehicleEntryInventoryResponse>
{
    public VehicleEntryInventoryController(ISender sender) : base(sender) { }
}
