using FluentValidation;

namespace Application.UseCase.Invoices;

// Caso de uso que modela una accion o consulta de negocio relacionada con GenerateInvoice.
public sealed class GenerateInvoiceValidator : AbstractValidator<GenerateInvoice>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public GenerateInvoiceValidator()
    {
        RuleFor(x => x.ServiceOrderId).GreaterThan(0).WithMessage("El identificador de la orden debe ser mayor que cero.");
        RuleFor(x => x.InvoiceStatusId).GreaterThan(0).WithMessage("El identificador del estado de factura debe ser mayor que cero.");
    }
}
