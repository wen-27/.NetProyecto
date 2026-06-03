using Api.DTOs.Neighborhoods;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Neighborhoods;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con Neighborhoods.
public sealed class NeighborhoodsController : CrudController<Neighborhood, CreateNeighborhoodRequest, UpdateNeighborhoodRequest, NeighborhoodResponse>
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public NeighborhoodsController(ISender sender) : base(sender) { }
}
