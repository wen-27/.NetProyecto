// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PersonEmailDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PersonEmails;

public sealed record CreatePersonEmailRequest(int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary);
public sealed record PersonEmailResponse(int Id, int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary);
