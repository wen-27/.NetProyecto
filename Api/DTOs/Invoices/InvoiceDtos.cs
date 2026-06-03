namespace Api.DTOs.Invoices;

// DTO usado para transportar datos de CreateInvoiceRequest entre la API y sus consumidores.
public sealed record CreateInvoiceRequest(
    string InvoiceNumber,
    int ServiceOrderId,
    int InvoiceStatusId,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    string? Observations);

// DTO usado para transportar datos de UpdateInvoiceRequest entre la API y sus consumidores.
public sealed record UpdateInvoiceRequest(
    string InvoiceNumber,
    int InvoiceStatusId,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    string? Observations);

// DTO usado para transportar datos de InvoiceResponse entre la API y sus consumidores.
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
