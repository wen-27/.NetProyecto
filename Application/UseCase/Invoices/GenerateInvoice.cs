using MediatR;

namespace Application.UseCase.Invoices;

public sealed record GenerateInvoice(int ServiceOrderId, int InvoiceStatusId) : IRequest<int>;
