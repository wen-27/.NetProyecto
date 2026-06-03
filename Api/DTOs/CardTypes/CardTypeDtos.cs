// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de CardTypeDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.CardTypes;

public sealed record CreateCardTypeRequest(string Name);
public sealed record UpdateCardTypeRequest(string Name);
public sealed record CardTypeResponse(int Id, string Name);
