// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con PersonRoles. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.DTOs.PersonRoles;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.PersonRoles;

public sealed class PersonRolesController : CrudController<PersonRole, CreatePersonRoleRequest, UpdatePersonRoleRequest, PersonRoleResponse>
{
    public PersonRolesController(ISender sender) : base(sender) { }
}
