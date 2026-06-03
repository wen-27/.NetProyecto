// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GenerateInvoiceValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
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
