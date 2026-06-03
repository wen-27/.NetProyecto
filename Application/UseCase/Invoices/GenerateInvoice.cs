using MediatR;

namespace Application.UseCase.Invoices;

// Caso de uso que modela una accion o consulta de negocio relacionada con GenerateInvoice.
public sealed record GenerateInvoice(int ServiceOrderId, int InvoiceStatusId) : IRequest<int>;
