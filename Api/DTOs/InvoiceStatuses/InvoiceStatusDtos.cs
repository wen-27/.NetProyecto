namespace Api.DTOs.InvoiceStatuses;

public sealed record CreateInvoiceStatusRequest(string Name);
public sealed record UpdateInvoiceStatusRequest(string Name);
public sealed record InvoiceStatusResponse(int Id, string Name);
