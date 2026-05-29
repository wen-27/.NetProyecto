namespace Application.DTOs;

public sealed record CreateAdditionalRequestDto(
    int RequestType,
    int? WorkshopServiceId,
    int? PartId,
    int? Quantity,
    string TechnicalComment);

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

public sealed record WorkshopChiefReviewRequestDto(string? Comment);
public sealed record ClientReviewAdditionalRequestDto(string? Comment);

public sealed record WorkshopServicePartDto(int PartId, int QuantityRequired);

public sealed record CreateWorkshopServiceDto(
    string Name,
    string Description,
    string Category,
    decimal LaborPercentage,
    IReadOnlyList<WorkshopServicePartDto> Parts);

public sealed record UpdateWorkshopServiceDto(
    string Name,
    string Description,
    string Category,
    decimal LaborPercentage,
    IReadOnlyList<WorkshopServicePartDto> Parts);

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

public sealed record WorkshopServicePartResponseDto(
    int PartId,
    string PartName,
    int QuantityRequired,
    decimal UnitSalePrice,
    decimal LineTotal);

public sealed record WorkshopServicePricePreviewDto(
    decimal PartsSubtotal,
    decimal LaborAmount,
    decimal FinalPrice);

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

public sealed record ReviewStockSubmissionDto(string? Comment);

public sealed record CreateClientPaymentDto(
    int InvoiceId,
    int PaymentMethodId,
    decimal Amount,
    string? Reference,
    string? CardType,
    string? CardLastFourDigits,
    string? CardHolderName,
    string? CardBrand);

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

public sealed record ReviewPaymentDto(DateTime? DeliveryDate, string? Comment);
public sealed record ConfirmDeliveryDateDto(DateTime DeliveryDate);

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

public sealed record OrderServiceDetailDto(
    int Id,
    string Name,
    string Status,
    decimal Price);

public sealed record MechanicOrderDetailDto(
    int Id,
    string Status,
    string? VehiclePlate,
    IReadOnlyList<OrderServiceDetailDto> Services);

public sealed record RecordMechanicWorkDto(string WorkPerformed);
