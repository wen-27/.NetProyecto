// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GenerateInvoice. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.Invoices;

public sealed record GenerateInvoice(int ServiceOrderId, int InvoiceStatusId) : IRequest<int>;
