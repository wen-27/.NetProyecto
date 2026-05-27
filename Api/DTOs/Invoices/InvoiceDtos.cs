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
