using Api.DTOs.Addresses;
using Api.DTOs.AuditActionTypes;
using Api.DTOs.Audits;
using Api.DTOs.CardTypes;
using Api.DTOs.Cities;
using Api.DTOs.Countries;
using Api.DTOs.Departments;
using Api.DTOs.DocumentTypes;
using Api.DTOs.EmailDomains;
using Api.DTOs.Genders;
using Api.DTOs.InvoiceDetails;
using Api.DTOs.InvoiceStatuses;
using Api.DTOs.Invoices;
using Api.DTOs.MechanicAssignments;
using Api.DTOs.MechanicSpecialties;
using Api.DTOs.MechanicSpecialtyAssignments;
using Api.DTOs.Neighborhoods;
using Api.DTOs.OrderServiceParts;
using Api.DTOs.OrderServices;
using Api.DTOs.OrderStatusHistory;
using Api.DTOs.OrderStatuses;
using Api.DTOs.PartBrands;
using Api.DTOs.PartCategories;
using Api.DTOs.PartPurchaseDetails;
using Api.DTOs.PartPurchases;
using Api.DTOs.Parts;
using Api.DTOs.PaymentCards;
using Api.DTOs.PaymentMethods;
using Api.DTOs.PaymentStatuses;
using Api.DTOs.Payments;
using Api.DTOs.PersonEmails;
using Api.DTOs.PersonPhones;
using Api.DTOs.PersonRoles;
using Api.DTOs.Persons;
using Api.DTOs.Roles;
using Api.DTOs.ServiceOrders;
using Api.DTOs.ServiceTypes;
using Api.DTOs.StreetTypes;
using Api.DTOs.Suppliers;
using Api.DTOs.Users;
using Api.DTOs.VehicleBrands;
using Api.DTOs.VehicleEntryInventory;
using Api.DTOs.VehicleModels;
using Api.DTOs.VehicleOwnerHistory;
using Api.DTOs.VehicleTypes;
using Api.DTOs.Vehicles;
using Domain.Entities;
using Mapster;

namespace Api.Mappings;

public sealed class ApiMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Country, CountryResponse>();
        config.NewConfig<Department, DepartmentResponse>();
        config.NewConfig<City, CityResponse>();
        config.NewConfig<Neighborhood, NeighborhoodResponse>();
        config.NewConfig<StreetType, StreetTypeResponse>();
        config.NewConfig<Address, AddressResponse>();
        config.NewConfig<DocumentType, DocumentTypeResponse>();
        config.NewConfig<Gender, GenderResponse>();
        config.NewConfig<Person, PersonResponse>()
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);
        config.NewConfig<EmailDomain, EmailDomainResponse>();
        config.NewConfig<PersonEmail, PersonEmailResponse>();
        config.NewConfig<PersonPhone, PersonPhoneResponse>()
            .Map(dest => dest.CountryId, src => src.CountryId);
        config.NewConfig<Role, RoleResponse>();
        config.NewConfig<PersonRole, PersonRoleResponse>();
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.IsActive, src => src.IsActive);
        config.NewConfig<MechanicSpecialty, MechanicSpecialtyResponse>();
        config.NewConfig<MechanicSpecialtyAssignment, MechanicSpecialtyAssignmentResponse>();
        config.NewConfig<VehicleType, VehicleTypeResponse>();
        config.NewConfig<VehicleBrand, VehicleBrandResponse>();
        config.NewConfig<VehicleModel, VehicleModelResponse>();
        config.NewConfig<Vehicle, VehicleResponse>();
        config.NewConfig<VehicleOwnerHistory, VehicleOwnerHistoryResponse>();
        config.NewConfig<ServiceType, ServiceTypeResponse>();
        config.NewConfig<OrderStatus, OrderStatusResponse>();
        config.NewConfig<ServiceOrder, ServiceOrderResponse>();
        config.NewConfig<VehicleEntryInventory, VehicleEntryInventoryResponse>();
        config.NewConfig<OrderService, OrderServiceResponse>();
        config.NewConfig<MechanicAssignment, MechanicAssignmentResponse>();
        config.NewConfig<OrderStatusHistory, OrderStatusHistoryResponse>()
            .Map(dest => dest.ChangedByUserId, src => src.UserId)
            .Map(dest => dest.ChangedAt, src => src.ChangeDate);
        config.NewConfig<PartCategory, PartCategoryResponse>();
        config.NewConfig<PartBrand, PartBrandResponse>();
        config.NewConfig<Part, PartResponse>();
        config.NewConfig<OrderServicePart, OrderServicePartResponse>();
        config.NewConfig<Supplier, SupplierResponse>()
            .Map(dest => dest.IsActive, src => src.Status);
        config.NewConfig<PartPurchase, PartPurchaseResponse>();
        config.NewConfig<PartPurchaseDetail, PartPurchaseDetailResponse>();
        config.NewConfig<InvoiceStatus, InvoiceStatusResponse>();
        config.NewConfig<Invoice, InvoiceResponse>();
        config.NewConfig<InvoiceDetail, InvoiceDetailResponse>();
        config.NewConfig<PaymentMethod, PaymentMethodResponse>();
        config.NewConfig<PaymentStatus, PaymentStatusResponse>();
        config.NewConfig<Payment, PaymentResponse>();
        config.NewConfig<CardType, CardTypeResponse>();
        config.NewConfig<PaymentCard, PaymentCardResponse>();
        config.NewConfig<AuditActionType, AuditActionTypeResponse>();
        config.NewConfig<Audit, AuditResponse>();
    }
}
