using Api.DTOs.StreetTypes;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.StreetTypes;

public sealed class StreetTypesController : CrudController<StreetType, CreateStreetTypeRequest, UpdateStreetTypeRequest, StreetTypeResponse>
{
    public StreetTypesController(ISender sender) : base(sender) { }
}
