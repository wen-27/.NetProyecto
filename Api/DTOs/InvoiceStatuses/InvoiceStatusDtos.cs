namespace Api.DTOs.InvoiceStatuses;

// DTO usado para transportar datos de CreateInvoiceStatusRequest entre la API y sus consumidores.
public sealed record CreateInvoiceStatusRequest(string Name);
// DTO usado para transportar datos de UpdateInvoiceStatusRequest entre la API y sus consumidores.
public sealed record UpdateInvoiceStatusRequest(string Name);
// DTO usado para transportar datos de InvoiceStatusResponse entre la API y sus consumidores.
public sealed record InvoiceStatusResponse(int Id, string Name);
