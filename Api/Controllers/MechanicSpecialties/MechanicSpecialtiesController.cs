using Api.DTOs.MechanicSpecialties;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.MechanicSpecialties;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con MechanicSpecialties.
public sealed class MechanicSpecialtiesController : CrudController<MechanicSpecialty, CreateMechanicSpecialtyRequest, UpdateMechanicSpecialtyRequest, MechanicSpecialtyResponse>
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public MechanicSpecialtiesController(ISender sender) : base(sender) { }
}
