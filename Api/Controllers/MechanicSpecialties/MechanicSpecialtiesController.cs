// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con MechanicSpecialties. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.DTOs.MechanicSpecialties;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.MechanicSpecialties;

public sealed class MechanicSpecialtiesController : CrudController<MechanicSpecialty, CreateMechanicSpecialtyRequest, UpdateMechanicSpecialtyRequest, MechanicSpecialtyResponse>
{
    public MechanicSpecialtiesController(ISender sender) : base(sender) { }
}
