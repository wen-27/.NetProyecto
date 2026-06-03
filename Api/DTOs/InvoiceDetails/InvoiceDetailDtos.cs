namespace Api.DTOs.InvoiceDetails;

// DTO usado para transportar datos de CreateInvoiceDetailRequest entre la API y sus consumidores.
public sealed record CreateInvoiceDetailRequest(
    int InvoiceId,
    int? SourcePartId,
    string Concept,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    string LineType);

// DTO usado para transportar datos de UpdateInvoiceDetailRequest entre la API y sus consumidores.
public sealed record UpdateInvoiceDetailRequest(
    int? SourcePartId,
    string Concept,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    string LineType);

// DTO usado para transportar datos de InvoiceDetailResponse entre la API y sus consumidores.
public sealed record InvoiceDetailResponse(
    int Id,
    int InvoiceId,
    int? SourcePartId,
    string Concept,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    string LineType);
