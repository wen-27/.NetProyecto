using Api.DTOs.Neighborhoods;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Neighborhoods;

public sealed class NeighborhoodsController : CrudController<Neighborhood, CreateNeighborhoodRequest, UpdateNeighborhoodRequest, NeighborhoodResponse>
{
    public NeighborhoodsController(ISender sender) : base(sender) { }
}
