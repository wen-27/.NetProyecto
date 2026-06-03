// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de InvoiceDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Invoices;

public sealed record CreateInvoiceRequest(
    string InvoiceNumber,
    int ServiceOrderId,
    int InvoiceStatusId,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    string? Observations);

public sealed record UpdateInvoiceRequest(
    string InvoiceNumber,
    int InvoiceStatusId,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    string? Observations);

public sealed record InvoiceResponse(
    int Id,
    string InvoiceNumber,
    int ServiceOrderId,
    int InvoiceStatusId,
    DateTime InvoiceDate,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    string? Observations);
