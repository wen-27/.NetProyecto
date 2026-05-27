using Api.DTOs.Addresses;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Addresses;

public sealed class AddressesController : CrudController<Address, CreateAddressRequest, UpdateAddressRequest, AddressResponse>
{
    public AddressesController(ISender sender) : base(sender) { }
}
