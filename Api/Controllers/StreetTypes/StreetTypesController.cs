using Api.DTOs.StreetTypes;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.StreetTypes;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con StreetTypes.
public sealed class StreetTypesController : CrudController<StreetType, CreateStreetTypeRequest, UpdateStreetTypeRequest, StreetTypeResponse>
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public StreetTypesController(ISender sender) : base(sender) { }
}
