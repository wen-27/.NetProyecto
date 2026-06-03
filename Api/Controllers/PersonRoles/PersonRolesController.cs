using Api.DTOs.PersonRoles;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.PersonRoles;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con PersonRoles.
public sealed class PersonRolesController : CrudController<PersonRole, CreatePersonRoleRequest, UpdatePersonRoleRequest, PersonRoleResponse>
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public PersonRolesController(ISender sender) : base(sender) { }
}
