// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con DeletePerson. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.Persons;

public sealed record DeletePerson(int Id) : IRequest;
