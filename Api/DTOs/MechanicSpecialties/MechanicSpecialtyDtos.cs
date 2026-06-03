// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de MechanicSpecialtyDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.MechanicSpecialties;

public sealed record CreateMechanicSpecialtyRequest(string Name);
public sealed record UpdateMechanicSpecialtyRequest(string Name);
public sealed record MechanicSpecialtyResponse(int Id, string Name);
