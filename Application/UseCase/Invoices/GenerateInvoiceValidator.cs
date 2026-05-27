using FluentValidation;

namespace Application.UseCase.Invoices;

public sealed class GenerateInvoiceValidator : AbstractValidator<GenerateInvoice>
{
    public GenerateInvoiceValidator()
    {
        RuleFor(x => x.ServiceOrderId).GreaterThan(0).WithMessage("El identificador de la orden debe ser mayor que cero.");
        RuleFor(x => x.InvoiceStatusId).GreaterThan(0).WithMessage("El identificador del estado de factura debe ser mayor que cero.");
    }
}
