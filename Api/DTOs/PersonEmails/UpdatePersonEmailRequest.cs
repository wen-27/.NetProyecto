// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdatePersonEmailRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PersonEmails;

public sealed record UpdatePersonEmailRequest(int EmailDomainId, string EmailUser, bool IsPrimary);
