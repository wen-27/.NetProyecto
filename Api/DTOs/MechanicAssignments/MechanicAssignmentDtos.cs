// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de MechanicAssignmentDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.MechanicAssignments;

public sealed record CreateMechanicAssignmentRequest(int OrderServiceId, int MechanicPersonId, int SpecialtyId);
public sealed record UpdateMechanicAssignmentRequest(int OrderServiceId, int MechanicPersonId, int SpecialtyId);
public sealed record MechanicAssignmentResponse(int Id, int OrderServiceId, int MechanicPersonId, int SpecialtyId);
