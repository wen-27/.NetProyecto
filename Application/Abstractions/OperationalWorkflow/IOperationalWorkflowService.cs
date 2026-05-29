using Application.DTOs;

namespace Application.Abstractions.OperationalWorkflow;

public interface IOperationalWorkflowService
{
    Task<IReadOnlyList<MechanicOrderDetailDto>> GetMechanicOrdersAsync(int mechanicPersonId, CancellationToken ct);
    Task<MechanicOrderDetailDto> GetMechanicOrderAsync(int mechanicPersonId, int orderId, CancellationToken ct);
    Task<IReadOnlyList<AdditionalRequestResponseDto>> GetMechanicRequestsAsync(int mechanicPersonId, CancellationToken ct);
    Task<AdditionalRequestResponseDto> CreateAdditionalRequestAsync(int mechanicPersonId, int orderId, CreateAdditionalRequestDto dto, CancellationToken ct);
    Task RecordMechanicWorkAsync(int mechanicPersonId, int orderId, RecordMechanicWorkDto dto, CancellationToken ct);
    Task CompleteMechanicOrderAsync(int mechanicPersonId, int orderId, RecordMechanicWorkDto dto, CancellationToken ct);
    Task UpdateMechanicOrderServiceStatusAsync(int mechanicPersonId, int orderServiceId, UpdateMechanicOrderServiceStatusDto dto, CancellationToken ct);
    Task<IReadOnlyList<MechanicDiagnosticResponseDto>> GetMechanicDiagnosticsAsync(int mechanicPersonId, CancellationToken ct);
    Task<MechanicDiagnosticResponseDto> SubmitMechanicDiagnosticAsync(int mechanicPersonId, int orderId, CreateMechanicDiagnosticDto dto, CancellationToken ct);

    Task<IReadOnlyList<AdditionalRequestResponseDto>> GetWorkshopChiefRequestsAsync(CancellationToken ct);
    Task<AdditionalRequestResponseDto> GetWorkshopChiefRequestAsync(int requestId, CancellationToken ct);
    Task<AdditionalRequestResponseDto> ApproveWorkshopChiefRequestAsync(int workshopChiefPersonId, int requestId, WorkshopChiefReviewRequestDto dto, CancellationToken ct);
    Task<AdditionalRequestResponseDto> RejectWorkshopChiefRequestAsync(int workshopChiefPersonId, int requestId, WorkshopChiefReviewRequestDto dto, CancellationToken ct);
    Task<IReadOnlyList<MechanicDiagnosticResponseDto>> GetWorkshopChiefDiagnosticsAsync(CancellationToken ct);
    Task<MechanicDiagnosticResponseDto> GetWorkshopChiefDiagnosticAsync(int diagnosticId, CancellationToken ct);
    Task<MechanicDiagnosticResponseDto> ApproveMechanicDiagnosticAsync(int workshopChiefPersonId, int diagnosticId, ReviewMechanicDiagnosticDto dto, CancellationToken ct);
    Task<MechanicDiagnosticResponseDto> RejectMechanicDiagnosticAsync(int workshopChiefPersonId, int diagnosticId, ReviewMechanicDiagnosticDto dto, CancellationToken ct);

    Task<IReadOnlyList<WorkshopServiceResponseDto>> GetWorkshopServicesAsync(CancellationToken ct);
    Task<WorkshopServiceResponseDto> GetWorkshopServiceAsync(int id, CancellationToken ct);
    Task<WorkshopServiceResponseDto> CreateWorkshopServiceAsync(CreateWorkshopServiceDto dto, CancellationToken ct);
    Task<WorkshopServiceResponseDto> UpdateWorkshopServiceAsync(int id, UpdateWorkshopServiceDto dto, CancellationToken ct);
    Task<WorkshopServiceResponseDto> SetWorkshopServiceStatusAsync(int id, bool active, CancellationToken ct);

    Task<IReadOnlyList<ClientOrderSummaryDto>> GetClientOrdersAsync(int clientPersonId, CancellationToken ct);
    Task<ClientOrderDetailDto> GetClientOrderAsync(int clientPersonId, int orderId, CancellationToken ct);
    Task<ClientOrderDetailDto> ApproveClientOrderAsync(int clientPersonId, int orderId, ClientReviewAdditionalRequestDto dto, CancellationToken ct);
    Task<ClientOrderDetailDto> RejectClientOrderAsync(int clientPersonId, int orderId, ClientReviewAdditionalRequestDto dto, CancellationToken ct);
    Task<IReadOnlyList<AdditionalRequestResponseDto>> GetClientApprovalsAsync(int clientPersonId, CancellationToken ct);
    Task<AdditionalRequestResponseDto> ApproveClientRequestAsync(int clientPersonId, int requestId, ClientReviewAdditionalRequestDto dto, CancellationToken ct);
    Task<AdditionalRequestResponseDto> RejectClientRequestAsync(int clientPersonId, int requestId, ClientReviewAdditionalRequestDto dto, CancellationToken ct);
    Task<IReadOnlyList<AdditionalRequestResponseDto>> GetClientMessagesAsync(int clientPersonId, CancellationToken ct);
    Task<IReadOnlyList<PaymentResponseDto>> GetClientPaymentsAsync(int clientPersonId, CancellationToken ct);
    Task<IReadOnlyList<BillingInvoiceResponseDto>> GetClientInvoicesAsync(int clientPersonId, CancellationToken ct);
    Task<PaymentResponseDto> CreateClientPaymentAsync(int clientPersonId, CreateClientPaymentDto dto, CancellationToken ct);

    Task<IReadOnlyList<BillingInvoiceResponseDto>> GetReceptionInvoicesAsync(CancellationToken ct);
    Task<IReadOnlyList<PaymentResponseDto>> GetPendingPaymentsAsync(CancellationToken ct);
    Task<PaymentResponseDto> GetPaymentAsync(int paymentId, CancellationToken ct);
    Task<PaymentResponseDto> ApprovePaymentAsync(int receptionistPersonId, int paymentId, ReviewPaymentDto dto, CancellationToken ct);
    Task<PaymentResponseDto> RejectPaymentAsync(int receptionistPersonId, int paymentId, ReviewPaymentDto dto, CancellationToken ct);
    Task ConfirmDeliveryDateAsync(int receptionistPersonId, int orderId, ConfirmDeliveryDateDto dto, CancellationToken ct);

    Task<IReadOnlyList<StockSubmissionResponseDto>> GetWarehouseSubmissionsAsync(int warehouseChiefPersonId, CancellationToken ct);
    Task<StockSubmissionResponseDto> GetWarehouseSubmissionAsync(int warehouseChiefPersonId, int id, CancellationToken ct);
    Task<StockSubmissionResponseDto> CreateStockSubmissionAsync(int warehouseChiefPersonId, CreateStockSubmissionDto dto, CancellationToken ct);
    Task<StockSubmissionResponseDto> UpdateStockSubmissionAsync(int warehouseChiefPersonId, int id, UpdateStockSubmissionDto dto, CancellationToken ct);
    Task<StockSubmissionResponseDto> SendStockSubmissionToReviewAsync(int warehouseChiefPersonId, int id, CancellationToken ct);

    Task<IReadOnlyList<StockSubmissionResponseDto>> GetInventoryReviewRequestsAsync(CancellationToken ct);
    Task<StockSubmissionResponseDto> GetInventoryReviewRequestAsync(int id, CancellationToken ct);
    Task<StockSubmissionResponseDto> ApproveStockSubmissionAsync(int inventoryManagerPersonId, int id, ReviewStockSubmissionDto dto, CancellationToken ct);
    Task<StockSubmissionResponseDto> RejectStockSubmissionAsync(int inventoryManagerPersonId, int id, ReviewStockSubmissionDto dto, CancellationToken ct);
}
