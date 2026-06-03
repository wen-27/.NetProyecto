// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de EmailDomainDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.EmailDomains;

public sealed record CreateEmailDomainRequest(string Domain);
public sealed record EmailDomainResponse(int Id, string Domain);
