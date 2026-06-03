// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GenerateInvoiceHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.Enums.OrderStatus;
using Domain.ValueObjects.Invoice;
using MediatR;

namespace Application.UseCase.Invoices;

public sealed class GenerateInvoiceHandler : IRequestHandler<GenerateInvoice, int>
{
    private readonly IInvoiceRepository _invoices;
    private readonly IOrderServicePartRepository _orderServiceParts;
    private readonly IPartRepository _parts;
    private readonly IOrderServiceRepository _orderServices;
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateInvoiceHandler(
        IInvoiceRepository invoices,
        IOrderServicePartRepository orderServiceParts,
        IPartRepository parts,
        IOrderServiceRepository orderServices,
        IServiceOrderRepository serviceOrders,
        IUnitOfWork unitOfWork)
    {
        _invoices = invoices;
        _orderServiceParts = orderServiceParts;
        _parts = parts;
        _orderServices = orderServices;
        _serviceOrders = serviceOrders;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(GenerateInvoice request, CancellationToken ct)
    {
        var serviceOrderId = new InvoiceServiceOrderId(request.ServiceOrderId);
        var invoiceStatusId = new InvoiceStatusId(request.InvoiceStatusId);
        var invoiceDate = new InvoiceDate(DateTime.UtcNow);

        if (await _invoices.ExistsServiceOrderIdAsync(serviceOrderId, ct))
        {
            throw new InvalidOperationException("La orden ya tiene una factura generada.");
        }

        var serviceOrder = await _serviceOrders.GetByIdAsync(serviceOrderId.Value, ct)
            ?? throw new KeyNotFoundException("No se encontró la orden de servicio.");

        if (serviceOrder.OrderStatusId != (int)ServiceOrderStatus.Completed)
        {
            throw new InvalidOperationException("Solo se puede generar factura para una orden completada.");
        }

        var orderServices = await _orderServices.GetByServiceOrderIdAsync(serviceOrderId.Value, ct);
        var orderParts = new List<OrderServicePart>();

        foreach (var orderService in orderServices)
        {
            orderParts.AddRange(await _orderServiceParts.GetByOrderServiceIdAsync(orderService.Id, ct));
        }

        var laborCost = new InvoiceLaborCost(orderServices.Sum(service => service.LaborCost));
        var partsTotal = orderParts.Sum(detail => detail.Quantity * detail.AppliedUnitPrice);
        var total = new InvoiceTotal(laborCost.Value + partsTotal);

        var invoice = new Invoice
        {
            ServiceOrderId = serviceOrderId.Value,
            InvoiceStatusId = invoiceStatusId.Value,
            InvoiceDate = invoiceDate.Value,
            LaborCost = laborCost.Value,
            Total = total.Value,
        };

        foreach (var orderService in orderServices)
        {
            invoice.Details.Add(new InvoiceDetail
            {
                Concept = string.IsNullOrWhiteSpace(orderService.Description)
                    ? $"Servicio #{orderService.ServiceTypeId}"
                    : orderService.Description,
                Quantity = 1,
                UnitPrice = orderService.LaborCost
            });
        }

        foreach (var orderPart in orderParts)
        {
            var part = await _parts.GetByIdAsync(orderPart.PartId, ct);
            var concept = part is null
                ? $"Repuesto #{orderPart.PartId}"
                : part.Description;

            invoice.Details.Add(new InvoiceDetail
            {
                Concept = concept,
                Quantity = orderPart.Quantity,
                UnitPrice = orderPart.AppliedUnitPrice
            });
        }

        await _invoices.AddAsync(invoice, ct);
        await _unitOfWork.CommitAsync(ct);

        return invoice.Id;
    }
}
