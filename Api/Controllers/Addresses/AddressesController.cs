using Api.DTOs.Addresses;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Addresses;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con Addresses.
public sealed class AddressesController : CrudController<Address, CreateAddressRequest, UpdateAddressRequest, AddressResponse>
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public AddressesController(ISender sender) : base(sender) { }
}
