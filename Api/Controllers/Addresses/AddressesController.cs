// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con Addresses. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.DTOs.Addresses;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Addresses;

public sealed class AddressesController : CrudController<Address, CreateAddressRequest, UpdateAddressRequest, AddressResponse>
{
    public AddressesController(ISender sender) : base(sender) { }
}
