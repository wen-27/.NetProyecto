// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdateServiceTypeRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.ServiceTypes;

public sealed record UpdateServiceTypeRequest(string Name, int EstimatedDays = 1);
