using Api.DTOs.Genders;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Genders;

public sealed class GendersController : CrudController<Gender, CreateGenderRequest, UpdateGenderRequest, GenderResponse>
{
    public GendersController(ISender sender) : base(sender) { }
}
