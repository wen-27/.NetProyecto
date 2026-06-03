// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de InvoiceStatusDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.InvoiceStatuses;

public sealed record CreateInvoiceStatusRequest(string Name);
public sealed record UpdateInvoiceStatusRequest(string Name);
public sealed record InvoiceStatusResponse(int Id, string Name);
