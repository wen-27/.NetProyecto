// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de InvoiceDetailDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.InvoiceDetails;

public sealed record CreateInvoiceDetailRequest(
    int InvoiceId,
    int? SourcePartId,
    string Concept,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    string LineType);

public sealed record UpdateInvoiceDetailRequest(
    int? SourcePartId,
    string Concept,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    string LineType);

public sealed record InvoiceDetailResponse(
    int Id,
    int InvoiceId,
    int? SourcePartId,
    string Concept,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    string LineType);
