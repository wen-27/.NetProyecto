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
