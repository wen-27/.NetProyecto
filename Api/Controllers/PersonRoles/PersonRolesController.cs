using Api.DTOs.PersonRoles;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.PersonRoles;

public sealed class PersonRolesController : CrudController<PersonRole, CreatePersonRoleRequest, UpdatePersonRoleRequest, PersonRoleResponse>
{
    public PersonRolesController(ISender sender) : base(sender) { }
}
