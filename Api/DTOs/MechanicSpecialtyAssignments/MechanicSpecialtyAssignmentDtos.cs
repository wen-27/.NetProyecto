// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de MechanicSpecialtyAssignmentDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.MechanicSpecialtyAssignments;

public sealed record CreateMechanicSpecialtyAssignmentRequest(int PersonId, int SpecialtyId);
public sealed record UpdateMechanicSpecialtyAssignmentRequest(int PersonId, int SpecialtyId);
public sealed record MechanicSpecialtyAssignmentResponse(int Id, int PersonId, int SpecialtyId);
