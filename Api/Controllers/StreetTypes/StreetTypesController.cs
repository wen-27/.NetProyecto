// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con StreetTypes. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.DTOs.StreetTypes;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.StreetTypes;

public sealed class StreetTypesController : CrudController<StreetType, CreateStreetTypeRequest, UpdateStreetTypeRequest, StreetTypeResponse>
{
    public StreetTypesController(ISender sender) : base(sender) { }
}
