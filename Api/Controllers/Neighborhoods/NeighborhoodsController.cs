// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con Neighborhoods. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.DTOs.Neighborhoods;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Neighborhoods;

public sealed class NeighborhoodsController : CrudController<Neighborhood, CreateNeighborhoodRequest, UpdateNeighborhoodRequest, NeighborhoodResponse>
{
    public NeighborhoodsController(ISender sender) : base(sender) { }
}
