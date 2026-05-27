using Api.DTOs.MechanicSpecialties;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.MechanicSpecialties;

public sealed class MechanicSpecialtiesController : CrudController<MechanicSpecialty, CreateMechanicSpecialtyRequest, UpdateMechanicSpecialtyRequest, MechanicSpecialtyResponse>
{
    public MechanicSpecialtiesController(ISender sender) : base(sender) { }
}
