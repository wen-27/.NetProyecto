using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Enums.OrderStatus;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public sealed class OperationalWorkflowService : IOperationalWorkflowService
{
    private readonly AppDbContext _context;

    public OperationalWorkflowService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MechanicOrderDetailDto>> GetMechanicOrdersAsync(int mechanicPersonId, CancellationToken ct)
    {
        var orderIds = await _context.MechanicAssignments
            .Where(x => x.MechanicPersonId == mechanicPersonId)
            .Select(x => x.OrderService.ServiceOrderId)
            .Distinct()
            .ToListAsync(ct);

        var orders = await OrderQuery()
            .Where(x => orderIds.Contains(x.Id))
            .OrderByDescending(x => x.EntryDate)
            .ToListAsync(ct);

        return orders.Select(ToMechanicOrderDetail).ToList();
    }

    public async Task<MechanicOrderDetailDto> GetMechanicOrderAsync(int mechanicPersonId, int orderId, CancellationToken ct)
    {
        await EnsureMechanicAssignedAsync(mechanicPersonId, orderId, ct);
        var order = await OrderQuery().FirstOrDefaultAsync(x => x.Id == orderId, ct)
            ?? throw new KeyNotFoundException("La orden no existe.");
        return ToMechanicOrderDetail(order);
    }

    public async Task<IReadOnlyList<AdditionalRequestResponseDto>> GetMechanicRequestsAsync(int mechanicPersonId, CancellationToken ct)
    {
        var requests = await RequestQuery()
            .Where(x => x.MechanicPersonId == mechanicPersonId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);
        return requests.Select(ToAdditionalRequest).ToList();
    }

    public async Task<AdditionalRequestResponseDto> CreateAdditionalRequestAsync(int mechanicPersonId, int orderId, CreateAdditionalRequestDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.TechnicalComment))
        {
            throw new InvalidOperationException("El comentario técnico es obligatorio.");
        }

        await EnsureMechanicAssignedAsync(mechanicPersonId, orderId, ct);

        var order = await _context.ServiceOrders
            .AsTracking()
            .Include(x => x.Vehicle)
            .ThenInclude(x => x.OwnerHistory)
            .FirstOrDefaultAsync(x => x.Id == orderId, ct)
            ?? throw new KeyNotFoundException("La orden no existe.");

        if (IsClosedOrder(order.OrderStatusId))
        {
            throw new InvalidOperationException("No se pueden crear solicitudes para órdenes canceladas, entregadas o cerradas.");
        }

        var clientPersonId = order.Vehicle.OwnerHistory
            .Where(x => x.EndDate == null)
            .OrderByDescending(x => x.StartDate)
            .Select(x => x.PersonId)
            .FirstOrDefault();

        if (clientPersonId == 0)
        {
            throw new InvalidOperationException("La orden no tiene un cliente propietario activo.");
        }

        var requestType = EnumValue<AdditionalRequestType>(dto.RequestType, "El tipo de solicitud no es válido.");
        ValidateAdditionalRequestShape(requestType, dto.WorkshopServiceId, dto.PartId, dto.Quantity);
        var estimatedPrice = await CalculateAdditionalRequestPriceAsync(requestType, dto.WorkshopServiceId, dto.PartId, dto.Quantity, ct);

        var request = new AdditionalServiceRequest
        {
            ServiceOrderId = orderId,
            MechanicPersonId = mechanicPersonId,
            ClientPersonId = clientPersonId,
            RequestType = requestType,
            Status = AdditionalRequestStatus.PendingWorkshopChiefApproval,
            WorkshopServiceId = dto.WorkshopServiceId,
            PartId = dto.PartId,
            Quantity = dto.Quantity,
            TechnicalComment = dto.TechnicalComment.Trim(),
            EstimatedPrice = estimatedPrice,
            CreatedAt = DateTime.UtcNow
        };

        await _context.AdditionalServiceRequests.AddAsync(request, ct);
        await _context.SaveChangesAsync(ct);

        return await GetWorkshopChiefRequestAsync(request.Id, ct);
    }

    public async Task RecordMechanicWorkAsync(int mechanicPersonId, int orderId, RecordMechanicWorkDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.WorkPerformed))
        {
            throw new InvalidOperationException("El trabajo realizado es obligatorio.");
        }

        await EnsureMechanicAssignedAsync(mechanicPersonId, orderId, ct);
        var order = await _context.ServiceOrders.AsTracking().FirstOrDefaultAsync(x => x.Id == orderId, ct)
            ?? throw new KeyNotFoundException("La orden no existe.");
        order.WorkPerformed = dto.WorkPerformed.Trim();
        order.OrderStatusId = (int)ServiceOrderStatus.InProgress;
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<AdditionalRequestResponseDto>> GetWorkshopChiefRequestsAsync(CancellationToken ct)
    {
        var requests = await RequestQuery()
            .Where(x => x.Status != AdditionalRequestStatus.Draft)
            .OrderByDescending(x => x.WorkshopChiefReviewedAt ?? x.CreatedAt)
            .ToListAsync(ct);
        return requests.Select(ToAdditionalRequest).ToList();
    }

    public async Task<AdditionalRequestResponseDto> GetWorkshopChiefRequestAsync(int requestId, CancellationToken ct)
    {
        var request = await RequestQuery().FirstOrDefaultAsync(x => x.Id == requestId, ct)
            ?? throw new KeyNotFoundException("La solicitud adicional no existe.");
        return ToAdditionalRequest(request);
    }

    public async Task<AdditionalRequestResponseDto> ApproveWorkshopChiefRequestAsync(int workshopChiefPersonId, int requestId, WorkshopChiefReviewRequestDto dto, CancellationToken ct)
    {
        var request = await _context.AdditionalServiceRequests.AsTracking().FirstOrDefaultAsync(x => x.Id == requestId, ct)
            ?? throw new KeyNotFoundException("La solicitud adicional no existe.");

        if (request.Status != AdditionalRequestStatus.PendingWorkshopChiefApproval)
        {
            throw new InvalidOperationException("La solicitud no está pendiente de aprobación técnica.");
        }

        request.Status = AdditionalRequestStatus.PendingClientApproval;
        request.WorkshopChiefPersonId = workshopChiefPersonId;
        request.WorkshopChiefComment = dto.Comment?.Trim();
        request.WorkshopChiefReviewedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
        return await GetWorkshopChiefRequestAsync(requestId, ct);
    }

    public async Task<AdditionalRequestResponseDto> RejectWorkshopChiefRequestAsync(int workshopChiefPersonId, int requestId, WorkshopChiefReviewRequestDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Comment))
        {
            throw new InvalidOperationException("El comentario es obligatorio al rechazar una solicitud.");
        }

        var request = await _context.AdditionalServiceRequests.AsTracking().FirstOrDefaultAsync(x => x.Id == requestId, ct)
            ?? throw new KeyNotFoundException("La solicitud adicional no existe.");

        if (request.Status != AdditionalRequestStatus.PendingWorkshopChiefApproval)
        {
            throw new InvalidOperationException("La solicitud no está pendiente de aprobación técnica.");
        }

        request.Status = AdditionalRequestStatus.RejectedByWorkshopChief;
        request.WorkshopChiefPersonId = workshopChiefPersonId;
        request.WorkshopChiefComment = dto.Comment.Trim();
        request.WorkshopChiefReviewedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
        return await GetWorkshopChiefRequestAsync(requestId, ct);
    }

    public async Task<IReadOnlyList<WorkshopServiceResponseDto>> GetWorkshopServicesAsync(CancellationToken ct)
    {
        var services = await WorkshopServiceQuery().OrderBy(x => x.Name).ToListAsync(ct);
        return services.Select(ToWorkshopService).ToList();
    }

    public async Task<WorkshopServiceResponseDto> GetWorkshopServiceAsync(int id, CancellationToken ct)
    {
        var service = await WorkshopServiceQuery().FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new KeyNotFoundException("El servicio de taller no existe.");
        return ToWorkshopService(service);
    }

    public async Task<WorkshopServiceResponseDto> CreateWorkshopServiceAsync(CreateWorkshopServiceDto dto, CancellationToken ct)
    {
        ValidateWorkshopService(dto.Name, dto.LaborPercentage, dto.Parts);
        await EnsureUniqueWorkshopServiceNameAsync(dto.Name, null, ct);

        var service = new WorkshopService
        {
            Name = dto.Name.Trim(),
            Description = dto.Description.Trim(),
            Category = dto.Category.Trim(),
            LaborPercentage = dto.LaborPercentage,
            Status = WorkshopServiceStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await ApplyWorkshopServicePartsAsync(service, dto.Parts, ct);
        await _context.WorkshopServices.AddAsync(service, ct);
        await _context.SaveChangesAsync(ct);
        return await GetWorkshopServiceAsync(service.Id, ct);
    }

    public async Task<WorkshopServiceResponseDto> UpdateWorkshopServiceAsync(int id, UpdateWorkshopServiceDto dto, CancellationToken ct)
    {
        ValidateWorkshopService(dto.Name, dto.LaborPercentage, dto.Parts);
        await EnsureUniqueWorkshopServiceNameAsync(dto.Name, id, ct);

        var service = await _context.WorkshopServices
            .AsTracking()
            .Include(x => x.Parts)
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new KeyNotFoundException("El servicio de taller no existe.");

        service.Name = dto.Name.Trim();
        service.Description = dto.Description.Trim();
        service.Category = dto.Category.Trim();
        service.LaborPercentage = dto.LaborPercentage;
        service.UpdatedAt = DateTime.UtcNow;
        _context.WorkshopServiceParts.RemoveRange(service.Parts);
        service.Parts.Clear();
        await ApplyWorkshopServicePartsAsync(service, dto.Parts, ct);
        await _context.SaveChangesAsync(ct);
        return await GetWorkshopServiceAsync(id, ct);
    }

    public async Task<WorkshopServiceResponseDto> SetWorkshopServiceStatusAsync(int id, bool active, CancellationToken ct)
    {
        var service = await _context.WorkshopServices.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new KeyNotFoundException("El servicio de taller no existe.");
        service.Status = active ? WorkshopServiceStatus.Active : WorkshopServiceStatus.Inactive;
        service.IsActive = active;
        service.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
        return await GetWorkshopServiceAsync(id, ct);
    }

    public async Task<IReadOnlyList<ClientOrderSummaryDto>> GetClientOrdersAsync(int clientPersonId, CancellationToken ct)
    {
        var orders = await ClientOrderQuery(clientPersonId)
            .Where(x =>
                x.OrderServices.Any() ||
                x.Invoice != null ||
                x.OrderStatusId >= (int)ServiceOrderStatus.WaitingForPayment ||
                (x.OrderStatusId == (int)ServiceOrderStatus.PendingClientApproval && x.EstimatedTotal > 0))
            .OrderByDescending(x => x.EntryDate)
            .ToListAsync(ct);
        return orders.Select(x =>
        {
            var payment = x.Invoice?.Payments.OrderByDescending(payment => payment.PaymentDate).FirstOrDefault();
            return new ClientOrderSummaryDto(
                x.Id,
                OrderCode(x),
                x.OrderStatus.Name,
                x.Vehicle.Vin,
                x.Vehicle.Vin,
                CurrentOwnerName(x),
                x.EstimatedTotal,
                x.EntryDate,
                x.DeliveryDate,
                x.Invoice?.Id,
                CanPay(x.Invoice, payment),
                payment?.PaymentStatus.Name,
                PaymentMessage(payment));
        }).ToList();
    }

    public async Task<ClientOrderDetailDto> GetClientOrderAsync(int clientPersonId, int orderId, CancellationToken ct)
    {
        var order = await ClientOrderQuery(clientPersonId).FirstOrDefaultAsync(x => x.Id == orderId, ct)
            ?? throw new UnauthorizedAccessException("No puedes consultar órdenes de otros clientes.");
        return ToClientOrderDetail(order);
    }

    public async Task<ClientOrderDetailDto> ApproveClientOrderAsync(int clientPersonId, int orderId, ClientReviewAdditionalRequestDto dto, CancellationToken ct)
    {
        var order = await ClientOrderQuery(clientPersonId)
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == orderId, ct)
            ?? throw new UnauthorizedAccessException("No puedes aprobar órdenes de otro cliente.");

        if (order.OrderStatusId != (int)ServiceOrderStatus.PendingClientApproval)
        {
            throw new InvalidOperationException("La orden no está pendiente de aprobación del cliente.");
        }

        if (!order.OrderServices.Any() && order.EstimatedTotal <= 0)
        {
            throw new InvalidOperationException("La orden aún no tiene servicios registrados para aprobar.");
        }

        await EnsureInvoiceForOrderAsync(order, ct);
        order.OrderStatusId = (int)ServiceOrderStatus.WaitingForPayment;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);

        var updatedOrder = await ClientOrderQuery(clientPersonId).FirstAsync(x => x.Id == orderId, ct);
        return ToClientOrderDetail(updatedOrder);
    }

    public async Task<ClientOrderDetailDto> RejectClientOrderAsync(int clientPersonId, int orderId, ClientReviewAdditionalRequestDto dto, CancellationToken ct)
    {
        var order = await ClientOrderQuery(clientPersonId)
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == orderId, ct)
            ?? throw new UnauthorizedAccessException("No puedes rechazar órdenes de otro cliente.");

        if (order.OrderStatusId != (int)ServiceOrderStatus.PendingClientApproval)
        {
            throw new InvalidOperationException("La orden no está pendiente de aprobación del cliente.");
        }

        order.OrderStatusId = (int)ServiceOrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);

        var updatedOrder = await ClientOrderQuery(clientPersonId).FirstAsync(x => x.Id == orderId, ct);
        return ToClientOrderDetail(updatedOrder);
    }

    public async Task<IReadOnlyList<AdditionalRequestResponseDto>> GetClientApprovalsAsync(int clientPersonId, CancellationToken ct)
    {
        var requests = await RequestQuery()
            .Where(x => x.ClientPersonId == clientPersonId && x.Status == AdditionalRequestStatus.PendingClientApproval)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(ct);
        return requests.Select(ToAdditionalRequest).ToList();
    }

    public async Task<AdditionalRequestResponseDto> ApproveClientRequestAsync(int clientPersonId, int requestId, ClientReviewAdditionalRequestDto dto, CancellationToken ct)
    {
        var request = await _context.AdditionalServiceRequests
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == requestId && x.ClientPersonId == clientPersonId, ct)
            ?? throw new UnauthorizedAccessException("No puedes aprobar solicitudes de otro cliente.");

        if (request.Status != AdditionalRequestStatus.PendingClientApproval)
        {
            throw new InvalidOperationException("La solicitud no está pendiente de aprobación del cliente.");
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        request.Status = AdditionalRequestStatus.ApprovedByClient;
        request.ClientComment = dto.Comment?.Trim();
        request.ClientReviewedAt = DateTime.UtcNow;
        await AddRequestToOrderAsync(request, ct);
        request.Status = AdditionalRequestStatus.AddedToOrder;
        request.AddedToOrderAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return await GetWorkshopChiefRequestAsync(requestId, ct);
    }

    public async Task<AdditionalRequestResponseDto> RejectClientRequestAsync(int clientPersonId, int requestId, ClientReviewAdditionalRequestDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Comment))
        {
            throw new InvalidOperationException("El comentario es obligatorio al rechazar una solicitud.");
        }

        var request = await _context.AdditionalServiceRequests.AsTracking().FirstOrDefaultAsync(x => x.Id == requestId && x.ClientPersonId == clientPersonId, ct)
            ?? throw new UnauthorizedAccessException("No puedes rechazar solicitudes de otro cliente.");

        if (request.Status != AdditionalRequestStatus.PendingClientApproval)
        {
            throw new InvalidOperationException("La solicitud no está pendiente de aprobación del cliente.");
        }

        request.Status = AdditionalRequestStatus.RejectedByClient;
        request.ClientComment = dto.Comment.Trim();
        request.ClientReviewedAt = DateTime.UtcNow;
        await MoveOrderOutOfClientApprovalIfNoPendingRequestsAsync(request.ServiceOrderId, request.Id, ct);
        await _context.SaveChangesAsync(ct);
        return await GetWorkshopChiefRequestAsync(requestId, ct);
    }

    public async Task<IReadOnlyList<AdditionalRequestResponseDto>> GetClientMessagesAsync(int clientPersonId, CancellationToken ct)
    {
        var requests = await RequestQuery()
            .Where(x => x.ClientPersonId == clientPersonId && x.Status != AdditionalRequestStatus.PendingWorkshopChiefApproval && x.Status != AdditionalRequestStatus.RejectedByWorkshopChief)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);
        return requests.Select(ToAdditionalRequest).ToList();
    }

    public async Task<IReadOnlyList<PaymentResponseDto>> GetClientPaymentsAsync(int clientPersonId, CancellationToken ct)
    {
        var payments = await PaymentQuery()
            .Where(x => x.ClientPersonId == clientPersonId)
            .OrderByDescending(x => x.PaymentDate)
            .ToListAsync(ct);
        return payments.Select(ToPayment).ToList();
    }

    public async Task<IReadOnlyList<BillingInvoiceResponseDto>> GetClientInvoicesAsync(int clientPersonId, CancellationToken ct)
    {
        var invoices = await InvoiceQuery()
            .Where(x => x.ServiceOrder.Vehicle.OwnerHistory.Any(owner => owner.PersonId == clientPersonId && owner.EndDate == null))
            .OrderByDescending(x => x.InvoiceDate)
            .ToListAsync(ct);
        return invoices.Select(ToBillingInvoice).ToList();
    }

    public async Task<PaymentResponseDto> CreateClientPaymentAsync(int clientPersonId, CreateClientPaymentDto dto, CancellationToken ct)
    {
        if (dto.Amount <= 0)
        {
            throw new InvalidOperationException("El monto debe ser mayor a cero.");
        }

        if (dto.CardLastFourDigits is not null && (dto.CardLastFourDigits.Length != 4 || !dto.CardLastFourDigits.All(char.IsDigit)))
        {
            throw new InvalidOperationException("Los últimos 4 dígitos de la tarjeta deben tener 4 números.");
        }

        var invoice = await _context.Invoices
            .AsTracking()
            .Include(x => x.ServiceOrder)
            .ThenInclude(x => x.Vehicle)
            .ThenInclude(x => x.OwnerHistory)
            .FirstOrDefaultAsync(x => x.Id == dto.InvoiceId, ct)
            ?? throw new KeyNotFoundException("La factura no existe.");

        var ownerId = invoice.ServiceOrder.Vehicle.OwnerHistory.FirstOrDefault(x => x.EndDate == null)?.PersonId;
        if (ownerId != clientPersonId)
        {
            throw new UnauthorizedAccessException("No puedes pagar facturas de otro cliente.");
        }

        var hasActivePayment = await _context.Payments.AnyAsync(x =>
            x.InvoiceId == dto.InvoiceId &&
            (x.PaymentStatusId == (int)PaymentStatusKind.Approved ||
             x.PaymentStatusId == (int)PaymentStatusKind.PendingReceptionVerification), ct);
        if (hasActivePayment)
        {
            throw new InvalidOperationException("La factura ya tiene un pago aprobado o pendiente de verificación.");
        }

        var methodExists = await _context.PaymentMethods.AnyAsync(x => x.Id == dto.PaymentMethodId, ct);
        if (!methodExists)
        {
            throw new InvalidOperationException("El método de pago no existe.");
        }

        var payment = new Payment
        {
            InvoiceId = dto.InvoiceId,
            PaymentMethodId = dto.PaymentMethodId,
            PaymentStatusId = (int)PaymentStatusKind.PendingReceptionVerification,
            PaymentDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            Amount = dto.Amount,
            Reference = string.IsNullOrWhiteSpace(dto.Reference)
                ? $"PAY-{dto.InvoiceId}-{DateTime.UtcNow:yyyyMMddHHmmss}"
                : dto.Reference.Trim(),
            ClientPersonId = clientPersonId
        };

        invoice.InvoiceStatusId = 2;
        invoice.ServiceOrder.OrderStatusId = (int)ServiceOrderStatus.PaymentUnderReview;
        await _context.Payments.AddAsync(payment, ct);
        await _context.SaveChangesAsync(ct);
        return await GetPaymentAsync(payment.Id, ct);
    }

    public async Task<IReadOnlyList<PaymentResponseDto>> GetPendingPaymentsAsync(CancellationToken ct)
    {
        var payments = await PaymentQuery()
            .Where(x => x.PaymentStatusId == (int)PaymentStatusKind.PendingReceptionVerification)
            .OrderBy(x => x.PaymentDate)
            .ToListAsync(ct);
        return payments.Select(ToPayment).ToList();
    }

    public async Task<IReadOnlyList<BillingInvoiceResponseDto>> GetReceptionInvoicesAsync(CancellationToken ct)
    {
        var invoices = await InvoiceQuery()
            .OrderByDescending(x => x.InvoiceDate)
            .ToListAsync(ct);
        return invoices.Select(ToBillingInvoice).ToList();
    }

    public async Task<PaymentResponseDto> GetPaymentAsync(int paymentId, CancellationToken ct)
    {
        var payment = await PaymentQuery().FirstOrDefaultAsync(x => x.Id == paymentId, ct)
            ?? throw new KeyNotFoundException("El pago no existe.");
        return ToPayment(payment);
    }

    public async Task<PaymentResponseDto> ApprovePaymentAsync(int receptionistPersonId, int paymentId, ReviewPaymentDto dto, CancellationToken ct)
    {
        if (dto.DeliveryDate is null)
        {
            throw new InvalidOperationException("La fecha de entrega es obligatoria al aprobar el pago.");
        }

        var payment = await _context.Payments
            .AsTracking()
            .Include(x => x.Invoice)
            .ThenInclude(x => x.ServiceOrder)
            .FirstOrDefaultAsync(x => x.Id == paymentId, ct)
            ?? throw new KeyNotFoundException("El pago no existe.");

        if (payment.PaymentStatusId != (int)PaymentStatusKind.PendingReceptionVerification)
        {
            throw new InvalidOperationException("El pago no está pendiente de verificación.");
        }

        payment.PaymentStatusId = (int)PaymentStatusKind.Approved;
        payment.VerifiedAt = DateTime.UtcNow;
        payment.VerifiedByReceptionistPersonId = receptionistPersonId;
        payment.DeliveryDate = dto.DeliveryDate;
        payment.Invoice.InvoiceStatusId = 3;
        payment.Invoice.ServiceOrder.OrderStatusId = (int)ServiceOrderStatus.ReadyForDelivery;
        payment.Invoice.ServiceOrder.DeliveryDate = dto.DeliveryDate;
        await _context.SaveChangesAsync(ct);
        return await GetPaymentAsync(paymentId, ct);
    }

    public async Task<PaymentResponseDto> RejectPaymentAsync(int receptionistPersonId, int paymentId, ReviewPaymentDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Comment))
        {
            throw new InvalidOperationException("El motivo de rechazo es obligatorio.");
        }

        var payment = await _context.Payments
            .AsTracking()
            .Include(x => x.Invoice)
            .ThenInclude(x => x.ServiceOrder)
            .FirstOrDefaultAsync(x => x.Id == paymentId, ct)
            ?? throw new KeyNotFoundException("El pago no existe.");

        if (payment.PaymentStatusId != (int)PaymentStatusKind.PendingReceptionVerification)
        {
            throw new InvalidOperationException("El pago no está pendiente de verificación.");
        }

        payment.PaymentStatusId = (int)PaymentStatusKind.Rejected;
        payment.VerifiedAt = DateTime.UtcNow;
        payment.VerifiedByReceptionistPersonId = receptionistPersonId;
        payment.RejectedReason = dto.Comment.Trim();
        payment.Invoice.ServiceOrder.OrderStatusId = (int)ServiceOrderStatus.WaitingForPayment;
        await _context.SaveChangesAsync(ct);
        return await GetPaymentAsync(paymentId, ct);
    }

    public async Task ConfirmDeliveryDateAsync(int receptionistPersonId, int orderId, ConfirmDeliveryDateDto dto, CancellationToken ct)
    {
        var order = await _context.ServiceOrders.AsTracking().FirstOrDefaultAsync(x => x.Id == orderId, ct)
            ?? throw new KeyNotFoundException("La orden no existe.");
        order.DeliveryDate = dto.DeliveryDate;
        if (order.OrderStatusId == (int)ServiceOrderStatus.Paid)
        {
            order.OrderStatusId = (int)ServiceOrderStatus.ReadyForDelivery;
        }
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<StockSubmissionResponseDto>> GetWarehouseSubmissionsAsync(int warehouseChiefPersonId, CancellationToken ct)
    {
        var submissions = await _context.StockSubmissions
            .Where(x => x.WarehouseChiefPersonId == warehouseChiefPersonId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);
        return submissions.Select(ToStockSubmission).ToList();
    }

    public async Task<StockSubmissionResponseDto> GetWarehouseSubmissionAsync(int warehouseChiefPersonId, int id, CancellationToken ct)
    {
        var submission = await _context.StockSubmissions.FirstOrDefaultAsync(x => x.Id == id && x.WarehouseChiefPersonId == warehouseChiefPersonId, ct)
            ?? throw new UnauthorizedAccessException("No puedes consultar envíos de otro jefe de bodega.");
        return ToStockSubmission(submission);
    }

    public async Task<StockSubmissionResponseDto> CreateStockSubmissionAsync(int warehouseChiefPersonId, CreateStockSubmissionDto dto, CancellationToken ct)
    {
        ValidateStockSubmission(dto.ProductName, dto.ReferenceCode, dto.SupplierPrice, dto.ProfitPercentage, dto.Quantity, dto.MinimumStock);
        await EnsureSupplierAsync(dto.SupplierId, ct);
        var salePrice = CalculateSalePrice(dto.SupplierPrice, dto.ProfitPercentage);
        var submission = new StockSubmission
        {
            WarehouseChiefPersonId = warehouseChiefPersonId,
            ProductName = dto.ProductName.Trim(),
            ReferenceCode = dto.ReferenceCode.Trim(),
            SupplierId = dto.SupplierId,
            SupplierPrice = dto.SupplierPrice,
            ProfitPercentage = dto.ProfitPercentage,
            SalePrice = salePrice,
            Quantity = dto.Quantity,
            MinimumStock = dto.MinimumStock,
            PartCategoryId = dto.PartCategoryId,
            PartBrandId = dto.PartBrandId,
            CategoryName = dto.CategoryName?.Trim(),
            BrandName = dto.BrandName?.Trim(),
            Description = dto.Description?.Trim(),
            WarehouseComment = dto.WarehouseComment?.Trim(),
            Status = StockSubmissionStatus.Draft,
            CreatedAt = DateTime.UtcNow
        };
        await _context.StockSubmissions.AddAsync(submission, ct);
        await _context.SaveChangesAsync(ct);
        return ToStockSubmission(submission);
    }

    public async Task<StockSubmissionResponseDto> UpdateStockSubmissionAsync(int warehouseChiefPersonId, int id, UpdateStockSubmissionDto dto, CancellationToken ct)
    {
        ValidateStockSubmission(dto.ProductName, dto.ReferenceCode, dto.SupplierPrice, dto.ProfitPercentage, dto.Quantity, dto.MinimumStock);
        var submission = await _context.StockSubmissions.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.WarehouseChiefPersonId == warehouseChiefPersonId, ct)
            ?? throw new UnauthorizedAccessException("No puedes actualizar envíos de otro jefe de bodega.");
        if (submission.Status is StockSubmissionStatus.ApprovedByInventoryManager or StockSubmissionStatus.AddedToInventory)
        {
            throw new InvalidOperationException("No se puede actualizar stock ya aprobado.");
        }

        await EnsureSupplierAsync(dto.SupplierId, ct);
        submission.ProductName = dto.ProductName.Trim();
        submission.ReferenceCode = dto.ReferenceCode.Trim();
        submission.SupplierId = dto.SupplierId;
        submission.SupplierPrice = dto.SupplierPrice;
        submission.ProfitPercentage = dto.ProfitPercentage;
        submission.SalePrice = CalculateSalePrice(dto.SupplierPrice, dto.ProfitPercentage);
        submission.Quantity = dto.Quantity;
        submission.MinimumStock = dto.MinimumStock;
        submission.PartCategoryId = dto.PartCategoryId;
        submission.PartBrandId = dto.PartBrandId;
        submission.CategoryName = dto.CategoryName?.Trim();
        submission.BrandName = dto.BrandName?.Trim();
        submission.Description = dto.Description?.Trim();
        submission.WarehouseComment = dto.WarehouseComment?.Trim();
        submission.UpdatedAt = DateTime.UtcNow;
        if (submission.Status == StockSubmissionStatus.RejectedByInventoryManager)
        {
            submission.Status = StockSubmissionStatus.Draft;
        }
        await _context.SaveChangesAsync(ct);
        return ToStockSubmission(submission);
    }

    public async Task<StockSubmissionResponseDto> SendStockSubmissionToReviewAsync(int warehouseChiefPersonId, int id, CancellationToken ct)
    {
        var submission = await _context.StockSubmissions.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.WarehouseChiefPersonId == warehouseChiefPersonId, ct)
            ?? throw new UnauthorizedAccessException("No puedes enviar stock de otro jefe de bodega.");
        if (submission.Status is not (StockSubmissionStatus.Draft or StockSubmissionStatus.RejectedByInventoryManager))
        {
            throw new InvalidOperationException("Solo se puede enviar a revisión stock en borrador o rechazado.");
        }
        submission.Status = StockSubmissionStatus.PendingInventoryManagerReview;
        await _context.SaveChangesAsync(ct);
        return ToStockSubmission(submission);
    }

    public async Task<IReadOnlyList<StockSubmissionResponseDto>> GetInventoryReviewRequestsAsync(CancellationToken ct)
    {
        var submissions = await _context.StockSubmissions
            .Where(x => x.Status == StockSubmissionStatus.PendingInventoryManagerReview)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(ct);
        return submissions.Select(ToStockSubmission).ToList();
    }

    public async Task<StockSubmissionResponseDto> GetInventoryReviewRequestAsync(int id, CancellationToken ct)
    {
        var submission = await _context.StockSubmissions.FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new KeyNotFoundException("La solicitud de stock no existe.");
        return ToStockSubmission(submission);
    }

    public async Task<StockSubmissionResponseDto> ApproveStockSubmissionAsync(int inventoryManagerPersonId, int id, ReviewStockSubmissionDto dto, CancellationToken ct)
    {
        var submission = await _context.StockSubmissions.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new KeyNotFoundException("La solicitud de stock no existe.");
        if (submission.Status != StockSubmissionStatus.PendingInventoryManagerReview)
        {
            throw new InvalidOperationException("La solicitud de stock no está pendiente de revisión.");
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        submission.Status = StockSubmissionStatus.ApprovedByInventoryManager;
        submission.InventoryManagerPersonId = inventoryManagerPersonId;
        submission.InventoryManagerComment = dto.Comment?.Trim();
        submission.ReviewedAt = DateTime.UtcNow;

        var part = await _context.Parts.AsTracking().FirstOrDefaultAsync(x => x.Code == submission.ReferenceCode, ct);
        if (part is null)
        {
            part = new Part
            {
                Code = submission.ReferenceCode,
                Description = string.IsNullOrWhiteSpace(submission.Description) ? submission.ProductName : submission.Description,
                PartCategoryId = submission.PartCategoryId ?? 1,
                PartBrandId = submission.PartBrandId,
                Stock = submission.Quantity,
                MinimumStock = submission.MinimumStock,
                UnitPrice = submission.SalePrice,
                IsActive = true
            };
            await _context.Parts.AddAsync(part, ct);
            await _context.SaveChangesAsync(ct);
        }
        else
        {
            part.Stock += submission.Quantity;
            part.MinimumStock = submission.MinimumStock;
            part.UnitPrice = submission.SalePrice;
            part.Description = string.IsNullOrWhiteSpace(submission.Description) ? part.Description : submission.Description;
        }

        await _context.InventoryHistory.AddAsync(new InventoryHistory
        {
            PartId = part.Id,
            StockSubmissionId = submission.Id,
            QuantityChange = submission.Quantity,
            ResultingStock = part.Stock,
            UnitPrice = submission.SalePrice,
            Action = "StockApproved",
            Comment = dto.Comment?.Trim(),
            CreatedAt = DateTime.UtcNow
        }, ct);
        submission.Status = StockSubmissionStatus.AddedToInventory;
        submission.AddedToInventoryAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return ToStockSubmission(submission);
    }

    public async Task<StockSubmissionResponseDto> RejectStockSubmissionAsync(int inventoryManagerPersonId, int id, ReviewStockSubmissionDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Comment))
        {
            throw new InvalidOperationException("El comentario es obligatorio al rechazar stock.");
        }

        var submission = await _context.StockSubmissions.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new KeyNotFoundException("La solicitud de stock no existe.");
        if (submission.Status != StockSubmissionStatus.PendingInventoryManagerReview)
        {
            throw new InvalidOperationException("La solicitud de stock no está pendiente de revisión.");
        }

        submission.Status = StockSubmissionStatus.RejectedByInventoryManager;
        submission.InventoryManagerPersonId = inventoryManagerPersonId;
        submission.InventoryManagerComment = dto.Comment.Trim();
        submission.ReviewedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
        return ToStockSubmission(submission);
    }

    private async Task AddRequestToOrderAsync(AdditionalServiceRequest request, CancellationToken ct)
    {
        if (request.Status == AdditionalRequestStatus.AddedToOrder || request.AddedToOrderAt.HasValue)
        {
            throw new InvalidOperationException("La solicitud ya fue añadida a la orden.");
        }

        var order = await _context.ServiceOrders.AsTracking().FirstOrDefaultAsync(x => x.Id == request.ServiceOrderId, ct)
            ?? throw new KeyNotFoundException("La orden no existe.");

        if (request.WorkshopServiceId.HasValue)
        {
            var service = await _context.WorkshopServices
                .Include(x => x.Parts)
                .ThenInclude(x => x.Part)
                .FirstOrDefaultAsync(x => x.Id == request.WorkshopServiceId.Value, ct)
                ?? throw new KeyNotFoundException("El servicio de taller no existe.");

            foreach (var servicePart in service.Parts)
            {
                if (servicePart.Part.Stock < servicePart.QuantityRequired)
                {
                    throw new InvalidOperationException($"No hay stock suficiente para {servicePart.Part.Description}.");
                }
            }

            var orderService = new OrderService
            {
                ServiceOrderId = order.Id,
                ServiceTypeId = 1,
                WorkshopServiceId = service.Id,
                Description = service.Description,
                LaborCost = service.LaborAmount,
                Price = service.PartsSubtotal,
                Status = OrderServiceStatus.Approved,
                CustomerApproved = true,
                ApprovalDate = DateTime.UtcNow
            };

            await _context.OrderServices.AddAsync(orderService, ct);
            await _context.SaveChangesAsync(ct);

            foreach (var servicePart in service.Parts)
            {
                var part = await _context.Parts.AsTracking().FirstAsync(x => x.Id == servicePart.PartId, ct);
                part.Stock -= servicePart.QuantityRequired;
                await _context.OrderServiceParts.AddAsync(new OrderServicePart
                {
                    OrderServiceId = orderService.Id,
                    PartId = part.Id,
                    Quantity = servicePart.QuantityRequired,
                    AppliedUnitPrice = servicePart.UnitSalePrice,
                    CustomerApproved = true,
                    ApprovalDate = DateTime.UtcNow
                }, ct);
            }
        }

        if (request.PartId.HasValue && request.Quantity.GetValueOrDefault() > 0 && !request.WorkshopServiceId.HasValue)
        {
            var part = await _context.Parts.AsTracking().FirstOrDefaultAsync(x => x.Id == request.PartId.Value, ct)
                ?? throw new KeyNotFoundException("El repuesto no existe.");
            if (part.Stock < request.Quantity!.Value)
            {
                throw new InvalidOperationException($"No hay stock suficiente para {part.Description}.");
            }

            var orderService = new OrderService
            {
                ServiceOrderId = order.Id,
                ServiceTypeId = 1,
                Description = $"Repuesto adicional: {part.Description}",
                LaborCost = 0,
                Price = part.UnitPrice * request.Quantity.Value,
                Status = OrderServiceStatus.Approved,
                CustomerApproved = true,
                ApprovalDate = DateTime.UtcNow
            };
            await _context.OrderServices.AddAsync(orderService, ct);
            await _context.SaveChangesAsync(ct);

            part.Stock -= request.Quantity.Value;
            await _context.OrderServiceParts.AddAsync(new OrderServicePart
            {
                OrderServiceId = orderService.Id,
                PartId = part.Id,
                Quantity = request.Quantity.Value,
                AppliedUnitPrice = part.UnitPrice,
                CustomerApproved = true,
                ApprovalDate = DateTime.UtcNow
            }, ct);
        }

        order.EstimatedTotal = await _context.OrderServices.Where(x => x.ServiceOrderId == order.Id).SumAsync(x => x.Price + x.LaborCost, ct);
        order.OrderStatusId = (int)ServiceOrderStatus.InProgress;
    }

    private async Task MoveOrderOutOfClientApprovalIfNoPendingRequestsAsync(int serviceOrderId, int currentRequestId, CancellationToken ct)
    {
        var hasPendingClientApproval = await _context.AdditionalServiceRequests.AnyAsync(x =>
            x.ServiceOrderId == serviceOrderId &&
            x.Id != currentRequestId &&
            x.Status == AdditionalRequestStatus.PendingClientApproval, ct);

        if (hasPendingClientApproval)
        {
            return;
        }

        var order = await _context.ServiceOrders.AsTracking().FirstOrDefaultAsync(x => x.Id == serviceOrderId, ct)
            ?? throw new KeyNotFoundException("La orden no existe.");

        if (order.OrderStatusId == (int)ServiceOrderStatus.PendingClientApproval)
        {
            order.OrderStatusId = (int)ServiceOrderStatus.InProgress;
        }
    }

    private IQueryable<ServiceOrder> OrderQuery()
    {
        return _context.ServiceOrders
            .Include(x => x.OrderStatus)
            .Include(x => x.Vehicle)
            .Include(x => x.OrderServices)
            .ThenInclude(x => x.ServiceType)
            .Include(x => x.OrderServices)
            .ThenInclude(x => x.WorkshopService);
    }

    private IQueryable<ServiceOrder> ClientOrderQuery(int clientPersonId)
    {
        return OrderQuery()
            .Include(x => x.Vehicle)
            .ThenInclude(x => x.OwnerHistory)
            .ThenInclude(x => x.Person)
            .Include(x => x.AdditionalServiceRequests)
            .ThenInclude(x => x.WorkshopService)
            .Include(x => x.AdditionalServiceRequests)
            .ThenInclude(x => x.Part)
            .Include(x => x.AdditionalServiceRequests)
            .ThenInclude(x => x.MechanicPerson)
            .Include(x => x.Invoice)
            .ThenInclude(x => x!.Payments)
            .ThenInclude(x => x.PaymentStatus)
            .Where(x => x.Vehicle.OwnerHistory.Any(owner => owner.PersonId == clientPersonId && owner.EndDate == null));
    }

    private IQueryable<AdditionalServiceRequest> RequestQuery()
    {
        return _context.AdditionalServiceRequests
            .Include(x => x.ServiceOrder)
            .ThenInclude(x => x.Vehicle)
            .ThenInclude(x => x.VehicleModel)
            .ThenInclude(x => x.VehicleBrand)
            .Include(x => x.ServiceOrder)
            .ThenInclude(x => x.Vehicle)
            .ThenInclude(x => x.OwnerHistory)
            .ThenInclude(x => x.Person)
            .Include(x => x.MechanicPerson)
            .Include(x => x.WorkshopService)
            .Include(x => x.Part);
    }

    private IQueryable<WorkshopService> WorkshopServiceQuery()
    {
        return _context.WorkshopServices
            .Include(x => x.Parts)
            .ThenInclude(x => x.Part);
    }

    private IQueryable<Payment> PaymentQuery()
    {
        return _context.Payments
            .Include(x => x.PaymentStatus)
            .Include(x => x.Invoice)
            .ThenInclude(x => x.ServiceOrder);
    }

    private IQueryable<Invoice> InvoiceQuery()
    {
        return _context.Invoices
            .Include(x => x.InvoiceStatus)
            .Include(x => x.ServiceOrder)
            .ThenInclude(x => x.Vehicle)
            .ThenInclude(x => x.OwnerHistory)
            .Include(x => x.Payments)
            .ThenInclude(x => x.PaymentStatus);
    }

    private async Task EnsureMechanicAssignedAsync(int mechanicPersonId, int orderId, CancellationToken ct)
    {
        var isAssigned = await _context.MechanicAssignments.AnyAsync(x =>
            x.MechanicPersonId == mechanicPersonId &&
            x.OrderService.ServiceOrderId == orderId, ct);

        if (!isAssigned)
        {
            throw new UnauthorizedAccessException("Solo el mecánico asignado puede operar esta orden.");
        }
    }

    private static bool IsClosedOrder(int statusId)
    {
        return statusId is (int)ServiceOrderStatus.Cancelled or (int)ServiceOrderStatus.Delivered or (int)ServiceOrderStatus.ReadyForDelivery;
    }

    private static void ValidateAdditionalRequestShape(AdditionalRequestType requestType, int? workshopServiceId, int? partId, int? quantity)
    {
        if ((requestType == AdditionalRequestType.Service || requestType == AdditionalRequestType.ServiceWithParts) && !workshopServiceId.HasValue)
        {
            throw new InvalidOperationException("Debe indicar el servicio de taller solicitado.");
        }

        if (requestType is AdditionalRequestType.Part && !partId.HasValue)
        {
            throw new InvalidOperationException("Debe indicar el repuesto solicitado.");
        }

        if (partId.HasValue && quantity.GetValueOrDefault() <= 0)
        {
            throw new InvalidOperationException("La cantidad debe ser mayor a cero.");
        }
    }

    private async Task<decimal> CalculateAdditionalRequestPriceAsync(AdditionalRequestType requestType, int? workshopServiceId, int? partId, int? quantity, CancellationToken ct)
    {
        decimal total = 0;
        if (workshopServiceId.HasValue)
        {
            total += await _context.WorkshopServices
                .Where(x => x.Id == workshopServiceId.Value && x.Status == WorkshopServiceStatus.Active)
                .Select(x => x.FinalPrice)
                .FirstOrDefaultAsync(ct);
            if (total <= 0)
            {
                throw new InvalidOperationException("El servicio de taller no existe o está inactivo.");
            }
        }

        if (requestType == AdditionalRequestType.Part && partId.HasValue)
        {
            var price = await _context.Parts.Where(x => x.Id == partId.Value && x.IsActive).Select(x => x.UnitPrice).FirstOrDefaultAsync(ct);
            if (price <= 0)
            {
                throw new InvalidOperationException("El repuesto no existe o está inactivo.");
            }
            total += price * quantity.GetValueOrDefault(1);
        }

        return total;
    }

    private async Task ApplyWorkshopServicePartsAsync(WorkshopService service, IReadOnlyList<WorkshopServicePartDto> parts, CancellationToken ct)
    {
        var partIds = parts.Select(x => x.PartId).Distinct().ToList();
        var dbParts = await _context.Parts.Where(x => partIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id, ct);
        if (dbParts.Count != partIds.Count)
        {
            throw new InvalidOperationException("Uno o más repuestos no existen.");
        }

        decimal subtotal = 0;
        foreach (var item in parts)
        {
            var part = dbParts[item.PartId];
            var lineTotal = part.UnitPrice * item.QuantityRequired;
            subtotal += lineTotal;
            service.Parts.Add(new WorkshopServicePart
            {
                WorkshopServiceId = service.Id,
                PartId = item.PartId,
                QuantityRequired = item.QuantityRequired,
                UnitSalePrice = part.UnitPrice,
                LineTotal = lineTotal
            });
        }

        service.PartsSubtotal = subtotal;
        service.LaborAmount = subtotal * service.LaborPercentage / 100m;
        service.FinalPrice = service.PartsSubtotal + service.LaborAmount;
    }

    private static void ValidateWorkshopService(string name, decimal laborPercentage, IReadOnlyList<WorkshopServicePartDto> parts)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("El nombre del servicio es obligatorio.");
        }
        if (laborPercentage < 0)
        {
            throw new InvalidOperationException("El porcentaje de mano de obra no puede ser negativo.");
        }
        if (parts.Any(x => x.QuantityRequired <= 0))
        {
            throw new InvalidOperationException("Cada repuesto debe tener cantidad mayor a cero.");
        }
        if (parts.GroupBy(x => x.PartId).Any(x => x.Count() > 1))
        {
            throw new InvalidOperationException("No se permiten repuestos duplicados en el mismo servicio.");
        }
    }

    private async Task EnsureUniqueWorkshopServiceNameAsync(string name, int? id, CancellationToken ct)
    {
        var exists = await _context.WorkshopServices.AnyAsync(x => x.Name == name.Trim() && (!id.HasValue || x.Id != id.Value), ct);
        if (exists)
        {
            throw new InvalidOperationException("Ya existe un servicio de taller con ese nombre.");
        }
    }

    private static void ValidateStockSubmission(string productName, string referenceCode, decimal supplierPrice, decimal profitPercentage, int quantity, int minimumStock)
    {
        if (string.IsNullOrWhiteSpace(productName)) throw new InvalidOperationException("El nombre del producto es obligatorio.");
        if (string.IsNullOrWhiteSpace(referenceCode)) throw new InvalidOperationException("El código de referencia es obligatorio.");
        if (supplierPrice <= 0) throw new InvalidOperationException("El precio del proveedor debe ser mayor a cero.");
        if (profitPercentage < 0) throw new InvalidOperationException("El porcentaje de ganancia no puede ser negativo.");
        if (quantity < 0) throw new InvalidOperationException("La cantidad no puede ser negativa.");
        if (minimumStock < 0) throw new InvalidOperationException("El stock mínimo no puede ser negativo.");
    }

    private async Task EnsureSupplierAsync(int supplierId, CancellationToken ct)
    {
        if (!await _context.Suppliers.AnyAsync(x => x.Id == supplierId, ct))
        {
            throw new InvalidOperationException("El proveedor no existe.");
        }
    }

    private static decimal CalculateSalePrice(decimal supplierPrice, decimal profitPercentage)
    {
        return supplierPrice + supplierPrice * profitPercentage / 100m;
    }

    private static TEnum EnumValue<TEnum>(int value, string message) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            throw new InvalidOperationException(message);
        }
        return (TEnum)Enum.ToObject(typeof(TEnum), value);
    }

    private static AdditionalRequestResponseDto ToAdditionalRequest(AdditionalServiceRequest request)
    {
        return new AdditionalRequestResponseDto(
            request.Id,
            request.ServiceOrderId,
            request.MechanicPersonId,
            request.ClientPersonId,
            request.ServiceOrder is null ? $"OT-{DateTime.UtcNow:yyyy}-{request.ServiceOrderId:0000}" : OrderCode(request.ServiceOrder),
            request.ServiceOrder is null ? "Cliente" : CurrentOwnerName(request.ServiceOrder),
            request.ServiceOrder?.Vehicle is null ? "Vehículo asociado a la orden" : VehicleDisplayName(request.ServiceOrder.Vehicle),
            PersonDisplayName(request.MechanicPerson, request.MechanicPersonId),
            request.RequestType.ToString(),
            request.Status.ToString(),
            request.WorkshopServiceId,
            request.WorkshopService?.Name,
            request.PartId,
            request.Part?.Description,
            request.Quantity,
            request.TechnicalComment,
            request.WorkshopChiefComment,
            request.ClientComment,
            request.EstimatedPrice,
            request.CreatedAt);
    }

    private static string VehicleDisplayName(Vehicle vehicle)
    {
        var model = vehicle.VehicleModel;
        var brand = model?.VehicleBrand?.BrandName;
        var name = string.Join(' ', new[] { brand, model?.ModelName, vehicle.Year > 0 ? vehicle.Year.ToString() : null }.Where(x => !string.IsNullOrWhiteSpace(x)));
        return string.IsNullOrWhiteSpace(name) ? vehicle.Vin : $"{name} · {vehicle.Vin}";
    }

    private static string PersonDisplayName(Person? person, int? personId = null)
    {
        if (person is null)
        {
            return personId.HasValue ? $"Persona #{personId.Value}" : "Sin mecánico asignado";
        }

        var name = string.Join(' ', new[] { person.FirstNames, person.LastNames }.Where(x => !string.IsNullOrWhiteSpace(x)));
        return string.IsNullOrWhiteSpace(name) ? $"Persona #{person.Id}" : name;
    }

    private static WorkshopServiceResponseDto ToWorkshopService(WorkshopService service)
    {
        return new WorkshopServiceResponseDto(
            service.Id,
            service.Name,
            service.Description,
            service.Category,
            service.LaborPercentage,
            service.PartsSubtotal,
            service.LaborAmount,
            service.FinalPrice,
            service.Status.ToString(),
            service.Parts.Select(x => new WorkshopServicePartResponseDto(x.PartId, x.Part.Description, x.QuantityRequired, x.UnitSalePrice, x.LineTotal)).ToList());
    }

    private static MechanicOrderDetailDto ToMechanicOrderDetail(ServiceOrder order)
    {
        return new MechanicOrderDetailDto(order.Id, order.OrderStatus.Name, order.Vehicle.Vin, order.OrderServices.Select(ToOrderServiceDetail).ToList());
    }

    private static ClientOrderDetailDto ToClientOrderDetail(ServiceOrder order)
    {
        var payment = order.Invoice?.Payments.OrderByDescending(x => x.PaymentDate).Select(ToPayment).FirstOrDefault();
        var latestPayment = order.Invoice?.Payments.OrderByDescending(x => x.PaymentDate).FirstOrDefault();
        return new ClientOrderDetailDto(
            order.Id,
            OrderCode(order),
            order.OrderStatus.Name,
            order.Vehicle.Vin,
            order.Vehicle.Vin,
            CurrentOwnerName(order),
            order.EstimatedTotal,
            order.EntryDate,
            order.DeliveryDate,
            order.Invoice?.Id,
            CanPay(order.Invoice, latestPayment),
            order.OrderServices.Select(ToOrderServiceDetail).ToList(),
            order.AdditionalServiceRequests.Where(x => x.Status == AdditionalRequestStatus.PendingClientApproval).Select(ToAdditionalRequest).ToList(),
            payment);
    }

    private static OrderServiceDetailDto ToOrderServiceDetail(OrderService service)
    {
        var name = service.WorkshopService?.Name ?? service.ServiceType.Name;
        return new OrderServiceDetailDto(service.Id, name, service.Status.ToString(), service.Price + service.LaborCost);
    }

    private static string OrderCode(ServiceOrder order)
    {
        return $"OT-{order.EntryDate:yyyy}-{order.Id:0000}";
    }

    private static string CurrentOwnerName(ServiceOrder order)
    {
        var owner = order.Vehicle.OwnerHistory.FirstOrDefault(x => x.EndDate == null)?.Person;
        if (owner is null)
        {
            return "Cliente";
        }

        return string.Join(' ', new[] { owner.FirstNames, owner.LastNames }.Where(x => !string.IsNullOrWhiteSpace(x)));
    }

    private static PaymentResponseDto ToPayment(Payment payment)
    {
        var message = PaymentMessage(payment);
        return new PaymentResponseDto(
            payment.Id,
            payment.InvoiceId,
            payment.Invoice.ServiceOrderId,
            payment.ClientPersonId,
            payment.Amount,
            payment.Reference ?? string.Empty,
            payment.PaymentStatus.Name,
            payment.PaymentDate,
            payment.VerifiedAt,
            payment.DeliveryDate,
            message);
    }

    private static BillingInvoiceResponseDto ToBillingInvoice(Invoice invoice)
    {
        var latestPayment = invoice.Payments.OrderByDescending(x => x.PaymentDate).FirstOrDefault();
        var subtotal = invoice.Total > 0 ? decimal.Round(invoice.Total / 1.19m, 2) : invoice.LaborCost;
        var taxes = invoice.Total > 0 ? invoice.Total - subtotal : 0m;
        return new BillingInvoiceResponseDto(
            invoice.Id,
            invoice.ServiceOrderId,
            $"OT-{DateTime.UtcNow:yyyy}-{invoice.ServiceOrderId:0000}",
            invoice.InvoiceStatusId,
            invoice.InvoiceStatus.Name,
            latestPayment?.PaymentStatus.Name ?? "PendingPayment",
            invoice.InvoiceDate,
            subtotal,
            taxes,
            invoice.Total,
            CanPay(invoice, latestPayment),
            latestPayment is null ? null : ToPayment(latestPayment));
    }

    private async Task EnsureInvoiceForOrderAsync(ServiceOrder order, CancellationToken ct)
    {
        if (order.Invoice is not null)
        {
            return;
        }

        var services = order.OrderServices.ToList();
        if (services.Count == 0 && order.EstimatedTotal <= 0)
        {
            throw new InvalidOperationException("La orden aún no tiene servicios registrados para facturar.");
        }

        var total = services.Sum(service => service.Price + service.LaborCost);
        if (total <= 0)
        {
            total = order.EstimatedTotal;
        }

        var invoice = new Invoice
        {
            ServiceOrderId = order.Id,
            InvoiceStatusId = 2,
            InvoiceDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            LaborCost = services.Sum(service => service.LaborCost),
            Total = total
        };

        foreach (var service in services)
        {
            invoice.Details.Add(new InvoiceDetail
            {
                Concept = service.WorkshopService?.Name ?? service.Description ?? $"Servicio #{service.ServiceTypeId}",
                Quantity = 1,
                UnitPrice = service.Price + service.LaborCost,
                CreatedAt = DateTime.UtcNow
            });
        }

        if (services.Count == 0)
        {
            invoice.Details.Add(new InvoiceDetail
            {
                Concept = $"Orden de servicio {OrderCode(order)}",
                Quantity = 1,
                UnitPrice = total,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.Invoices.AddAsync(invoice, ct);
        order.Invoice = invoice;
    }

    private static bool CanPay(Invoice? invoice, Payment? latestPayment)
    {
        if (invoice is null)
        {
            return false;
        }

        if (latestPayment is null)
        {
            return true;
        }

        return latestPayment.PaymentStatusId == (int)PaymentStatusKind.Rejected ||
               latestPayment.PaymentStatusId == (int)PaymentStatusKind.Refunded;
    }

    private static string? PaymentMessage(Payment? payment)
    {
        if (payment is null)
        {
            return null;
        }

        return payment.PaymentStatusId switch
        {
            (int)PaymentStatusKind.PendingReceptionVerification => "Pago enviado. Tu pago está pendiente de verificación por recepción.",
            (int)PaymentStatusKind.Approved => "Pago exitoso. Tu pago fue verificado por recepción.",
            (int)PaymentStatusKind.Rejected => "Pago rechazado. Puedes intentarlo nuevamente o comunicarte con recepción.",
            _ => null
        };
    }

    private static StockSubmissionResponseDto ToStockSubmission(StockSubmission submission)
    {
        return new StockSubmissionResponseDto(
            submission.Id,
            submission.ProductName,
            submission.ReferenceCode,
            submission.SupplierId,
            submission.SupplierPrice,
            submission.ProfitPercentage,
            submission.SalePrice,
            submission.Quantity,
            submission.MinimumStock,
            submission.Status.ToString(),
            submission.WarehouseComment,
            submission.InventoryManagerComment,
            submission.CreatedAt);
    }
}
