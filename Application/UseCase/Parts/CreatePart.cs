// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreatePart. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.Parts;

public sealed record CreatePart(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice) : IRequest<int>;
