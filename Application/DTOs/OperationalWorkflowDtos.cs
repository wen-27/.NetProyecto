namespace Application.DTOs;

// DTO usado para transportar datos de CreateAdditionalRequestDto entre la API y sus consumidores.
public sealed record CreateAdditionalRequestDto(
    int RequestType,
    int? WorkshopServiceId,
    int? PartId,
    int? Quantity,
    string TechnicalComment);

// DTO usado para transportar datos de AdditionalRequestResponseDto entre la API y sus consumidores.
public sealed record AdditionalRequestResponseDto(
    int Id,
    int ServiceOrderId,
    int MechanicPersonId,
    int? ClientPersonId,
    string OrderCode,
    string Customer,
    string Vehicle,
    string Mechanic,
    string RequestType,
    string Status,
    int? WorkshopServiceId,
    string? WorkshopServiceName,
    int? PartId,
    string? PartName,
    int? Quantity,
    string TechnicalComment,
    string? WorkshopChiefComment,
    string? ClientComment,
    decimal EstimatedPrice,
    DateTime CreatedAt);

// DTO usado para transportar datos de WorkshopChiefReviewRequestDto entre la API y sus consumidores.
public sealed record WorkshopChiefReviewRequestDto(string? Comment);
// DTO usado para transportar datos de ClientReviewAdditionalRequestDto entre la API y sus consumidores.
public sealed record ClientReviewAdditionalRequestDto(string? Comment);

// DTO usado para transportar datos de WorkshopServicePartDto entre la API y sus consumidores.
public sealed record WorkshopServicePartDto(int PartId, int QuantityRequired);

// DTO usado para transportar datos de CreateWorkshopServiceDto entre la API y sus consumidores.
public sealed record CreateWorkshopServiceDto(
    string Name,
    string Description,
    string Category,
    decimal LaborPercentage,
    IReadOnlyList<WorkshopServicePartDto> Parts);

// DTO usado para transportar datos de UpdateWorkshopServiceDto entre la API y sus consumidores.
public sealed record UpdateWorkshopServiceDto(
    string Name,
    string Description,
    string Category,
    decimal LaborPercentage,
    IReadOnlyList<WorkshopServicePartDto> Parts);

// DTO usado para transportar datos de WorkshopServiceResponseDto entre la API y sus consumidores.
public sealed record WorkshopServiceResponseDto(
    int Id,
    string Name,
    string Description,
    string Category,
    decimal LaborPercentage,
    decimal PartsSubtotal,
    decimal LaborAmount,
    decimal FinalPrice,
    string Status,
    IReadOnlyList<WorkshopServicePartResponseDto> Parts);

// DTO usado para transportar datos de WorkshopServicePartResponseDto entre la API y sus consumidores.
public sealed record WorkshopServicePartResponseDto(
    int PartId,
    string PartName,
    int QuantityRequired,
    decimal UnitSalePrice,
    decimal LineTotal);

// DTO usado para transportar datos de WorkshopServicePricePreviewDto entre la API y sus consumidores.
public sealed record WorkshopServicePricePreviewDto(
    decimal PartsSubtotal,
    decimal LaborAmount,
    decimal FinalPrice);

// DTO usado para transportar datos de CreateStockSubmissionDto entre la API y sus consumidores.
public sealed record CreateStockSubmissionDto(
    string ProductName,
    string ReferenceCode,
    int SupplierId,
    decimal SupplierPrice,
    decimal ProfitPercentage,
    int Quantity,
    int MinimumStock,
    int? PartCategoryId,
    int? PartBrandId,
    string? CategoryName,
    string? BrandName,
    string? Description,
    string? WarehouseComment);

// DTO usado para transportar datos de UpdateStockSubmissionDto entre la API y sus consumidores.
public sealed record UpdateStockSubmissionDto(
    string ProductName,
    string ReferenceCode,
    int SupplierId,
    decimal SupplierPrice,
    decimal ProfitPercentage,
    int Quantity,
    int MinimumStock,
    int? PartCategoryId,
    int? PartBrandId,
    string? CategoryName,
    string? BrandName,
    string? Description,
    string? WarehouseComment);

// DTO usado para transportar datos de StockSubmissionResponseDto entre la API y sus consumidores.
public sealed record StockSubmissionResponseDto(
    int Id,
    string ProductName,
    string ReferenceCode,
    int SupplierId,
    decimal SupplierPrice,
    decimal ProfitPercentage,
    decimal SalePrice,
    int Quantity,
    int MinimumStock,
    string Status,
    string? WarehouseComment,
    string? InventoryManagerComment,
    DateTime CreatedAt);

// DTO usado para transportar datos de ReviewStockSubmissionDto entre la API y sus consumidores.
public sealed record ReviewStockSubmissionDto(string? Comment);

// DTO usado para transportar datos de CreateClientPaymentDto entre la API y sus consumidores.
public sealed record CreateClientPaymentDto(
    int InvoiceId,
    int PaymentMethodId,
    decimal Amount,
    string? Reference,
    string? CardType,
    string? CardLastFourDigits,
    string? CardHolderName,
    string? CardBrand);

// DTO usado para transportar datos de PaymentResponseDto entre la API y sus consumidores.
public sealed record PaymentResponseDto(
    int Id,
    int InvoiceId,
    int ServiceOrderId,
    int? ClientPersonId,
    decimal Amount,
    string Reference,
    string Status,
    DateTime CreatedAt,
    DateTime? VerifiedAt,
    DateTime? DeliveryDate,
    string? Message);

// DTO usado para transportar datos de BillingInvoiceResponseDto entre la API y sus consumidores.
public sealed record BillingInvoiceResponseDto(
    int Id,
    int ServiceOrderId,
    string OrderCode,
    int InvoiceStatusId,
    string InvoiceStatus,
    string PaymentStatus,
    DateTime InvoiceDate,
    decimal Subtotal,
    decimal Taxes,
    decimal Total,
    bool CanPay,
    PaymentResponseDto? LatestPayment);

// DTO usado para transportar datos de ReviewPaymentDto entre la API y sus consumidores.
public sealed record ReviewPaymentDto(DateTime? DeliveryDate, string? Comment);
// DTO usado para transportar datos de ConfirmDeliveryDateDto entre la API y sus consumidores.
public sealed record ConfirmDeliveryDateDto(DateTime DeliveryDate);

// DTO usado para transportar datos de ClientOrderSummaryDto entre la API y sus consumidores.
public sealed record ClientOrderSummaryDto(
    int Id,
    string Code,
    string Status,
    string? VehiclePlate,
    string Vehicle,
    string Customer,
    decimal EstimatedTotal,
    DateTime EntryDate,
    DateTime? DeliveryDate,
    int? InvoiceId,
    bool CanPay,
    string? PaymentStatus,
    string? PaymentMessage);

// DTO usado para transportar datos de ClientOrderDetailDto entre la API y sus consumidores.
public sealed record ClientOrderDetailDto(
    int Id,
    string Code,
    string Status,
    string? VehiclePlate,
    string Vehicle,
    string Customer,
    decimal EstimatedTotal,
    DateTime EntryDate,
    DateTime? DeliveryDate,
    int? InvoiceId,
    bool CanPay,
    IReadOnlyList<OrderServiceDetailDto> Services,
    IReadOnlyList<AdditionalRequestResponseDto> PendingApprovals,
    PaymentResponseDto? Payment);

// DTO usado para transportar datos de OrderServiceDetailDto entre la API y sus consumidores.
public sealed record OrderServiceDetailDto(
    int Id,
    string Name,
    string Status,
    decimal Price);

// DTO usado para transportar datos de MechanicOrderDetailDto entre la API y sus consumidores.
public sealed record MechanicOrderDetailDto(
    int Id,
    string Code,
    string Status,
    string? VehiclePlate,
    string Vehicle,
    string Customer,
    decimal EstimatedTotal,
    DateTime EntryDate,
    DateTime? EstimatedDeliveryDate,
    IReadOnlyList<OrderServiceDetailDto> Services);

// DTO usado para transportar datos de RecordMechanicWorkDto entre la API y sus consumidores.
public sealed record RecordMechanicWorkDto(string WorkPerformed);

// DTO usado para transportar datos de UpdateMechanicOrderServiceStatusDto entre la API y sus consumidores.
public sealed record UpdateMechanicOrderServiceStatusDto(string Status);

// DTO usado para transportar datos de CreateMechanicDiagnosticDto entre la API y sus consumidores.
public sealed record CreateMechanicDiagnosticDto(
    string Findings,
    string RecommendedWork);

// DTO usado para transportar datos de ReviewMechanicDiagnosticDto entre la API y sus consumidores.
public sealed record ReviewMechanicDiagnosticDto(string? Comment);

// DTO usado para transportar datos de MechanicDiagnosticResponseDto entre la API y sus consumidores.
public sealed record MechanicDiagnosticResponseDto(
    int Id,
    int ServiceOrderId,
    string OrderCode,
    string Customer,
    string Vehicle,
    int MechanicPersonId,
    string Mechanic,
    string Status,
    string Findings,
    string RecommendedWork,
    string? WorkshopChiefComment,
    DateTime SubmittedAt,
    DateTime? ReviewedAt);
