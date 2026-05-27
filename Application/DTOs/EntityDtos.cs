using Domain.Entities;

namespace Application.DTOs;

public sealed record AuditActionTypeDto(int Id, string Name);

public sealed record AuditDto(int Id, int UserId, int AuditActionTypeId, string AffectedEntity, int AffectedRecordId, string? Description);

public sealed record CardTypeDto(int Id, string Name);

public sealed record CityDto(int Id, int DepartmentId, string Name);

public sealed record DepartmentDto(int Id, string Name);

public sealed record DocumentTypeDto(int Id, string Code, string Name);

public sealed record EmailDomainDto(int Id, string Domain);

public sealed record InvoiceDto(int Id, int ServiceOrderId, int InvoiceStatusId, DateTime InvoiceDate, decimal LaborCost, decimal Total);

public sealed record InvoiceDetailDto(int Id, int InvoiceId, string Concept, int Quantity, decimal UnitPrice);

public sealed record InvoiceStatusDto(int Id, string Name);

public sealed record OrderStatusDto(int Id, string Name);

public sealed record OrderStatusHistoryDto(int Id, int ServiceOrderId, int? PreviousOrderStatusId, int NewOrderStatusId, int UserId, DateTime ChangeDate, string? Observation);

public sealed record PartCategoryDto(int Id, string Name);

public sealed record PartBrandDto(int Id, string Name);

public sealed record PartDto(int Id, int PartCategoryId, int? PartBrandId, string Code, string Description, int Stock, int MinimumStock, decimal UnitPrice, bool IsActive);

public sealed record PartPurchaseDto(int Id, int SupplierId, DateTime PurchaseDate, decimal Total);

public sealed record PartPurchaseDetailDto(int Id, int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);

public sealed record PaymentDto(int Id, int InvoiceId, int PaymentMethodId, int PaymentStatusId, DateTime PaymentDate, decimal Amount, string? Reference);

public sealed record PaymentCardDto(int Id, int PaymentId, int CardTypeId, string LastFourDigits, string CardHolder, string? AuthorizationCode);

public sealed record PaymentMethodDto(int Id, string Name);

public sealed record PaymentStatusDto(int Id, string Name);

public sealed record PersonDto(int Id, string FirstNames, string LastNames, DateTime RegistrationDate);

public sealed record PersonEmailDto(int Id, int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary);

public sealed record PersonPhoneDto(int Id, int PersonId, int CountryId, string PhoneNumber, bool IsPrimary);

public sealed record RoleDto(int Id, string RoleName);

public sealed record ServiceOrderDto(
    int Id,
    int VehicleId,
    int OrderStatusId,
    DateTime EntryDate,
    DateTime? EstimatedDeliveryDate,
    string? WorkPerformed);

public sealed record ServiceTypeDto(int Id, string Name, int EstimatedDays);

public sealed record SupplierDto(int Id, string Name, string? TaxId, string? Phone, string? Email, bool Status);

public sealed record UserDto(int Id, int PersonId, bool Status);

public sealed record UserRoleDto(int UserId, int RoleId);

public sealed record VehicleBrandDto(int Id, string BrandName);

public sealed record VehicleDto(int Id, int ModelId, int VehicleTypeId, string Vin, int Year, string? Color, int Mileage, bool IsActive);

public sealed record VehicleModelDto(int Id, int BrandId, string ModelName);

public sealed record VehicleOwnerHistoryDto(int Id, int VehicleId, int PersonId, DateTime StartDate, DateTime? EndDate);

public sealed record VehicleTypeDto(int Id, string Name);

public static class EntityDtoMapper
{
    public static AuditActionTypeDto ToDto(this AuditActionType entity) => new(entity.Id, entity.Name);

    public static AuditDto ToDto(this Audit entity) => new(entity.Id, entity.UserId, entity.AuditActionTypeId, entity.AffectedEntity, entity.AffectedRecordId, entity.Description);

    public static CardTypeDto ToDto(this CardType entity) => new(entity.Id, entity.Name);

    public static CityDto ToDto(this City entity) => new(entity.Id, entity.DepartmentId, entity.Name);

    public static DepartmentDto ToDto(this Department entity) => new(entity.Id, entity.Name);

    public static DocumentTypeDto ToDto(this DocumentType entity) => new(entity.Id, entity.Code, entity.Name);

    public static EmailDomainDto ToDto(this EmailDomain entity) => new(entity.Id, entity.Domain);

    public static InvoiceDto ToDto(this Invoice entity) => new(entity.Id, entity.ServiceOrderId, entity.InvoiceStatusId, entity.InvoiceDate, entity.LaborCost, entity.Total);

    public static InvoiceDetailDto ToDto(this InvoiceDetail entity) => new(entity.Id, entity.InvoiceId, entity.Concept, entity.Quantity, entity.UnitPrice);

    public static InvoiceStatusDto ToDto(this InvoiceStatus entity) => new(entity.Id, entity.Name);

    public static OrderStatusDto ToDto(this OrderStatus entity) => new(entity.Id, entity.Name);

    public static OrderStatusHistoryDto ToDto(this OrderStatusHistory entity) => new(entity.Id, entity.ServiceOrderId, entity.PreviousOrderStatusId, entity.NewOrderStatusId, entity.UserId, entity.ChangeDate, entity.Observation);

    public static PartCategoryDto ToDto(this PartCategory entity) => new(entity.Id, entity.Name);

    public static PartBrandDto ToDto(this PartBrand entity) => new(entity.Id, entity.Name);

    public static PartDto ToDto(this Part entity) => new(entity.Id, entity.PartCategoryId, entity.PartBrandId, entity.Code, entity.Description, entity.Stock, entity.MinimumStock, entity.UnitPrice, entity.IsActive);

    public static PartPurchaseDto ToDto(this PartPurchase entity) => new(entity.Id, entity.SupplierId, entity.PurchaseDate, entity.Total);

    public static PartPurchaseDetailDto ToDto(this PartPurchaseDetail entity) => new(entity.Id, entity.PartPurchaseId, entity.PartId, entity.Quantity, entity.UnitPrice);

    public static PaymentDto ToDto(this Payment entity) => new(entity.Id, entity.InvoiceId, entity.PaymentMethodId, entity.PaymentStatusId, entity.PaymentDate, entity.Amount, entity.Reference);

    public static PaymentCardDto ToDto(this PaymentCard entity) => new(entity.Id, entity.PaymentId, entity.CardTypeId, entity.LastFourDigits, entity.CardHolder, entity.AuthorizationCode);

    public static PaymentMethodDto ToDto(this PaymentMethod entity) => new(entity.Id, entity.Name);

    public static PaymentStatusDto ToDto(this PaymentStatus entity) => new(entity.Id, entity.Name);

    public static PersonDto ToDto(this Person entity) => new(entity.Id, entity.FirstNames, entity.LastNames, entity.RegistrationDate);

    public static PersonEmailDto ToDto(this PersonEmail entity) => new(entity.Id, entity.PersonId, entity.EmailDomainId, entity.EmailUser, entity.IsPrimary);

    public static PersonPhoneDto ToDto(this PersonPhone entity) => new(entity.Id, entity.PersonId, entity.CountryId, entity.PhoneNumber, entity.IsPrimary);

    public static RoleDto ToDto(this Role entity) => new(entity.Id, entity.RoleName);

    public static ServiceOrderDto ToDto(this ServiceOrder entity) => new(entity.Id, entity.VehicleId, entity.OrderStatusId, entity.EntryDate, entity.EstimatedDeliveryDate, entity.WorkPerformed);

    public static ServiceTypeDto ToDto(this ServiceType entity) => new(entity.Id, entity.Name, entity.EstimatedDays);

    public static SupplierDto ToDto(this Supplier entity) => new(entity.Id, entity.Name, entity.TaxId, entity.Phone, entity.Email, entity.Status);

    public static UserDto ToDto(this User entity) => new(entity.Id, entity.PersonId, entity.Status);

    public static UserRoleDto ToDto(this UserRole entity) => new(entity.UserId, entity.RoleId);

    public static VehicleBrandDto ToDto(this VehicleBrand entity) => new(entity.Id, entity.BrandName);

    public static VehicleDto ToDto(this Vehicle entity) => new(entity.Id, entity.ModelId, entity.VehicleTypeId, entity.Vin, entity.Year, entity.Color, entity.Mileage, entity.IsActive);

    public static VehicleModelDto ToDto(this VehicleModel entity) => new(entity.Id, entity.BrandId, entity.ModelName);

    public static VehicleOwnerHistoryDto ToDto(this VehicleOwnerHistory entity) => new(entity.Id, entity.VehicleId, entity.PersonId, entity.StartDate, entity.EndDate);

    public static VehicleTypeDto ToDto(this VehicleType entity) => new(entity.Id, entity.Name);
}
