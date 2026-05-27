using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Audit;
using Domain.ValueObjects.AuditActionType;
using Domain.ValueObjects.DocumentType;
using Domain.ValueObjects.EmailDomain;
using Domain.ValueObjects.Invoice;
using Domain.ValueObjects.InvoiceDetail;
using Domain.ValueObjects.OrderStatus;
using Domain.ValueObjects.Part;
using Domain.ValueObjects.PartCategory;
using Domain.ValueObjects.PersonEmail;
using Domain.ValueObjects.PersonPhone;
using Domain.ValueObjects.Role;
using Domain.ValueObjects.ServiceOrder;
using Domain.ValueObjects.ServiceType;
using Domain.ValueObjects.User;
using Domain.ValueObjects.UserRole;
using Domain.ValueObjects.VehicleBrand;
using Domain.ValueObjects.VehicleModel;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class AuditRepository : GenericRepository<Audit>, IAuditRepository
{
    public AuditRepository(AppDbContext context) : base(context) { }
    public Task<IReadOnlyList<Audit>> GetByUserIdAsync(AuditUserId userId, CancellationToken ct = default) => ListByAsync(nameof(Audit.UserId), userId, ct);
    public Task<IReadOnlyList<Audit>> GetByAffectedEntityAsync(AuditAffectedEntity affectedEntity, CancellationToken ct = default) => ListByAsync(nameof(Audit.AffectedEntity), affectedEntity, ct);
}

public sealed class AuditActionTypeRepository : GenericRepository<AuditActionType>, IAuditActionTypeRepository
{
    public AuditActionTypeRepository(AppDbContext context) : base(context) { }
    public Task<AuditActionType?> GetByNameAsync(AuditActionTypeName name, CancellationToken ct = default) => FirstByAsync(nameof(AuditActionType.Name), name, ct);
    public Task<bool> ExistsNameAsync(AuditActionTypeName name, CancellationToken ct = default) => ExistsByAsync(nameof(AuditActionType.Name), name, ct);
}

public sealed class CardTypeRepository : GenericRepository<CardType>, ICardTypeRepository { public CardTypeRepository(AppDbContext context) : base(context) { } }
public sealed class CityRepository : GenericRepository<City>, ICityRepository { public CityRepository(AppDbContext context) : base(context) { } }
public sealed class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository { public DepartmentRepository(AppDbContext context) : base(context) { } }
public sealed class InvoiceStatusRepository : GenericRepository<InvoiceStatus>, IInvoiceStatusRepository { public InvoiceStatusRepository(AppDbContext context) : base(context) { } }
public sealed class OrderStatusHistoryRepository : GenericRepository<OrderStatusHistory>, IOrderStatusHistoryRepository { public OrderStatusHistoryRepository(AppDbContext context) : base(context) { } }
public sealed class PartBrandRepository : GenericRepository<PartBrand>, IPartBrandRepository { public PartBrandRepository(AppDbContext context) : base(context) { } }
public sealed class PartPurchaseRepository : GenericRepository<PartPurchase>, IPartPurchaseRepository { public PartPurchaseRepository(AppDbContext context) : base(context) { } }
public sealed class PartPurchaseDetailRepository : GenericRepository<PartPurchaseDetail>, IPartPurchaseDetailRepository { public PartPurchaseDetailRepository(AppDbContext context) : base(context) { } }
public sealed class PaymentRepository : GenericRepository<Payment>, IPaymentRepository { public PaymentRepository(AppDbContext context) : base(context) { } }
public sealed class PaymentCardRepository : GenericRepository<PaymentCard>, IPaymentCardRepository { public PaymentCardRepository(AppDbContext context) : base(context) { } }
public sealed class PaymentMethodRepository : GenericRepository<PaymentMethod>, IPaymentMethodRepository { public PaymentMethodRepository(AppDbContext context) : base(context) { } }
public sealed class PaymentStatusRepository : GenericRepository<PaymentStatus>, IPaymentStatusRepository { public PaymentStatusRepository(AppDbContext context) : base(context) { } }
public sealed class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext context) : base(context) { }

    public Task<bool> HasActiveServiceOrdersAsCurrentOwnerAsync(int personId, CancellationToken ct = default)
    {
        return Context.VehicleOwnerHistory
            .AsNoTracking()
            .Where(owner => owner.PersonId == personId && owner.EndDate == null)
            .AnyAsync(owner => Context.ServiceOrders
                .Any(order => order.VehicleId == owner.VehicleId &&
                    order.OrderStatus.Name != "Completed" &&
                    order.OrderStatus.Name != "Cancelled" &&
                    order.OrderStatus.Name != "Voided"), ct);
    }

    public Task<bool> HasActiveServiceOrdersAsMechanicAsync(int personId, CancellationToken ct = default)
    {
        return Context.MechanicAssignments
            .AsNoTracking()
            .Include(assignment => assignment.OrderService)
            .ThenInclude(orderService => orderService.ServiceOrder)
            .ThenInclude(order => order.OrderStatus)
            .AnyAsync(assignment => assignment.MechanicPersonId == personId &&
                assignment.OrderService.ServiceOrder.OrderStatus.Name != "Completed" &&
                assignment.OrderService.ServiceOrder.OrderStatus.Name != "Cancelled" &&
                assignment.OrderService.ServiceOrder.OrderStatus.Name != "Voided", ct);
    }
}
public sealed class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository { public SupplierRepository(AppDbContext context) : base(context) { } }
public sealed class VehicleTypeRepository : GenericRepository<VehicleType>, IVehicleTypeRepository { public VehicleTypeRepository(AppDbContext context) : base(context) { } }

public sealed class MechanicAssignmentRepository : GenericRepository<MechanicAssignment>, IMechanicAssignmentRepository
{
    public MechanicAssignmentRepository(AppDbContext context) : base(context) { }

    public async Task<bool> HasActiveAssignmentAsync(int mechanicPersonId, int orderServiceId, CancellationToken ct = default)
    {
        var targetOrderService = await Context.OrderServices
            .AsNoTracking()
            .Where(x => x.Id == orderServiceId)
            .Select(x => new { x.ServiceOrderId })
            .FirstOrDefaultAsync(ct);

        if (targetOrderService is null)
        {
            return false;
        }

        return await Context.MechanicAssignments
            .AsNoTracking()
            .Include(x => x.OrderService)
            .ThenInclude(x => x.ServiceOrder)
            .ThenInclude(x => x.OrderStatus)
            .AnyAsync(x => x.MechanicPersonId == mechanicPersonId
                && x.OrderService.ServiceOrderId != targetOrderService.ServiceOrderId
                && x.OrderService.ServiceOrder.OrderStatus.Name != "Completed"
                && x.OrderService.ServiceOrder.OrderStatus.Name != "Cancelled"
                && x.OrderService.ServiceOrder.OrderStatus.Name != "Voided", ct);
    }
}

public sealed class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
{
    public DocumentTypeRepository(AppDbContext context) : base(context) { }
    public Task<DocumentType?> GetByCodeAsync(DocumentTypeCode code, CancellationToken ct = default) => FirstByAsync(nameof(DocumentType.Code), code, ct);
    public Task<DocumentType?> GetByNameAsync(DocumentTypeName name, CancellationToken ct = default) => FirstByAsync(nameof(DocumentType.Name), name, ct);
    public Task<bool> ExistsCodeAsync(DocumentTypeCode code, CancellationToken ct = default) => ExistsByAsync(nameof(DocumentType.Code), code, ct);
    public Task<bool> ExistsNameAsync(DocumentTypeName name, CancellationToken ct = default) => ExistsByAsync(nameof(DocumentType.Name), name, ct);
}

public sealed class EmailDomainRepository : GenericRepository<EmailDomain>, IEmailDomainRepository
{
    public EmailDomainRepository(AppDbContext context) : base(context) { }
    public Task<EmailDomain?> GetByDomainAsync(EmailDomainValue domain, CancellationToken ct = default) => FirstByAsync(nameof(EmailDomain.Domain), domain, ct);
    public Task<bool> ExistsDomainAsync(EmailDomainValue domain, CancellationToken ct = default) => ExistsByAsync(nameof(EmailDomain.Domain), domain, ct);
}

public sealed class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(AppDbContext context) : base(context) { }
    public Task<Invoice?> GetByServiceOrderIdAsync(InvoiceServiceOrderId serviceOrderId, CancellationToken ct = default) => FirstByAsync(nameof(Invoice.ServiceOrderId), serviceOrderId, ct);
    public Task<bool> ExistsServiceOrderIdAsync(InvoiceServiceOrderId serviceOrderId, CancellationToken ct = default) => ExistsByAsync(nameof(Invoice.ServiceOrderId), serviceOrderId, ct);
}

public sealed class InvoiceDetailRepository : GenericRepository<InvoiceDetail>, IInvoiceDetailRepository
{
    public InvoiceDetailRepository(AppDbContext context) : base(context) { }
    public Task<IReadOnlyList<InvoiceDetail>> GetByInvoiceIdAsync(InvoiceDetailInvoiceId invoiceId, CancellationToken ct = default) => ListByAsync(nameof(InvoiceDetail.InvoiceId), invoiceId, ct);
}

public sealed class OrderServiceRepository : GenericRepository<OrderService>, IOrderServiceRepository
{
    public OrderServiceRepository(AppDbContext context) : base(context) { }
    public Task<IReadOnlyList<OrderService>> GetByServiceOrderIdAsync(int serviceOrderId, CancellationToken ct = default) => ListByAsync(nameof(OrderService.ServiceOrderId), serviceOrderId, ct);
    public Task<bool> ExistsServiceOrderAndServiceTypeAsync(int serviceOrderId, int serviceTypeId, CancellationToken ct = default) => ExistsByAsync(nameof(OrderService.ServiceOrderId), serviceOrderId, nameof(OrderService.ServiceTypeId), serviceTypeId, ct);
}

public sealed class OrderServicePartRepository : GenericRepository<OrderServicePart>, IOrderServicePartRepository
{
    public OrderServicePartRepository(AppDbContext context) : base(context) { }
    public Task<IReadOnlyList<OrderServicePart>> GetByOrderServiceIdAsync(int orderServiceId, CancellationToken ct = default) => ListByAsync(nameof(OrderServicePart.OrderServiceId), orderServiceId, ct);
    public Task<bool> ExistsOrderServiceAndPartAsync(int orderServiceId, int partId, CancellationToken ct = default) => ExistsByAsync(nameof(OrderServicePart.OrderServiceId), orderServiceId, nameof(OrderServicePart.PartId), partId, ct);
}

public sealed class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
{
    public OrderStatusRepository(AppDbContext context) : base(context) { }
    public Task<OrderStatus?> GetByNameAsync(OrderStatusName name, CancellationToken ct = default) => FirstByAsync(nameof(OrderStatus.Name), name, ct);
    public Task<bool> ExistsNameAsync(OrderStatusName name, CancellationToken ct = default) => ExistsByAsync(nameof(OrderStatus.Name), name, ct);
}

public sealed class PartRepository : GenericRepository<Part>, IPartRepository
{
    public PartRepository(AppDbContext context) : base(context) { }
    public Task<Part?> GetByCodeAsync(PartCode code, CancellationToken ct = default) => FirstByAsync(nameof(Part.Code), code, ct);
    public Task<IReadOnlyList<Part>> GetByCategoryIdAsync(PartCategoryId categoryId, CancellationToken ct = default) => ListByAsync(nameof(Part.PartCategoryId), categoryId, ct);
    public async Task<IReadOnlyList<Part>> GetLowStockAsync(CancellationToken ct = default) => await Context.Parts.AsNoTracking().Where(x => x.Stock <= x.MinimumStock).OrderBy(x => x.Id).ToListAsync(ct);
    public Task<bool> ExistsCodeAsync(PartCode code, CancellationToken ct = default) => ExistsByAsync(nameof(Part.Code), code, ct);
}

public sealed class PartCategoryRepository : GenericRepository<PartCategory>, IPartCategoryRepository
{
    public PartCategoryRepository(AppDbContext context) : base(context) { }
    public Task<PartCategory?> GetByNameAsync(PartCategoryName name, CancellationToken ct = default) => FirstByAsync(nameof(PartCategory.Name), name, ct);
    public Task<bool> ExistsNameAsync(PartCategoryName name, CancellationToken ct = default) => ExistsByAsync(nameof(PartCategory.Name), name, ct);
}

public sealed class PersonEmailRepository : GenericRepository<PersonEmail>, IPersonEmailRepository
{
    public PersonEmailRepository(AppDbContext context) : base(context) { }
    public Task<PersonEmail?> GetByEmailAsync(PersonEmailUser emailUser, PersonEmailDomainId emailDomainId, CancellationToken ct = default) => FirstByAsync(nameof(PersonEmail.EmailUser), emailUser, nameof(PersonEmail.EmailDomainId), emailDomainId, ct);
    public Task<IReadOnlyList<PersonEmail>> GetByPersonIdAsync(PersonEmailPersonId personId, CancellationToken ct = default) => ListByAsync(nameof(PersonEmail.PersonId), personId, ct);
    public Task<bool> ExistsEmailAsync(PersonEmailUser emailUser, PersonEmailDomainId emailDomainId, CancellationToken ct = default) => ExistsByAsync(nameof(PersonEmail.EmailUser), emailUser, nameof(PersonEmail.EmailDomainId), emailDomainId, ct);
}

public sealed class PersonPhoneRepository : GenericRepository<PersonPhone>, IPersonPhoneRepository
{
    public PersonPhoneRepository(AppDbContext context) : base(context) { }
    public Task<PersonPhone?> GetByPhoneAsync(PersonPhoneCountryId countryId, PersonPhoneNumber phoneNumber, CancellationToken ct = default) => FirstByAsync(nameof(PersonPhone.CountryId), countryId, nameof(PersonPhone.PhoneNumber), phoneNumber, ct);
    public Task<IReadOnlyList<PersonPhone>> GetByPersonIdAsync(PersonPhonePersonId personId, CancellationToken ct = default) => ListByAsync(nameof(PersonPhone.PersonId), personId, ct);
    public Task<bool> ExistsPhoneAsync(PersonPhoneCountryId countryId, PersonPhoneNumber phoneNumber, CancellationToken ct = default) => ExistsByAsync(nameof(PersonPhone.CountryId), countryId, nameof(PersonPhone.PhoneNumber), phoneNumber, ct);
}

public sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context) { }
    public Task<Role?> GetByNameAsync(RoleName name, CancellationToken ct = default) => FirstByAsync(nameof(Role.RoleName), name, ct);
    public Task<bool> ExistsNameAsync(RoleName name, CancellationToken ct = default) => ExistsByAsync(nameof(Role.RoleName), name, ct);
}

public sealed class ServiceOrderRepository : GenericRepository<ServiceOrder>, IServiceOrderRepository
{
    public ServiceOrderRepository(AppDbContext context) : base(context) { }
    public Task<IReadOnlyList<ServiceOrder>> GetByVehicleIdAsync(ServiceOrderVehicleId vehicleId, CancellationToken ct = default) => ListByAsync(nameof(ServiceOrder.VehicleId), vehicleId, ct);
    public Task<IReadOnlyList<ServiceOrder>> GetByStatusIdAsync(ServiceOrderStatusId statusId, CancellationToken ct = default) => ListByAsync(nameof(ServiceOrder.OrderStatusId), statusId, ct);
    public Task<bool> HasActiveOrderForVehicleAsync(ServiceOrderVehicleId vehicleId, CancellationToken ct = default)
    {
        var id = (int)ValueOf(vehicleId);
        return Context.ServiceOrders.Include(x => x.OrderStatus).AnyAsync(x => x.VehicleId == id && x.OrderStatus.Name != "Completed" && x.OrderStatus.Name != "Cancelled" && x.OrderStatus.Name != "Voided", ct);
    }

    public async Task<IReadOnlyList<ServiceOrder>> GetFilteredAsync(
        int page,
        int pageSize,
        string? search = null,
        int? clientPersonId = null,
        string? vin = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        int? statusId = null,
        int? mechanicPersonId = null,
        CancellationToken ct = default)
    {
        return await ApplyFilters(search, clientPersonId, vin, fromDate, toDate, statusId, mechanicPersonId)
            .OrderByDescending(x => x.EntryDate)
            .ThenByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<int> CountFilteredAsync(
        string? search = null,
        int? clientPersonId = null,
        string? vin = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        int? statusId = null,
        int? mechanicPersonId = null,
        CancellationToken ct = default)
    {
        return ApplyFilters(search, clientPersonId, vin, fromDate, toDate, statusId, mechanicPersonId).CountAsync(ct);
    }

    private IQueryable<ServiceOrder> ApplyFilters(
        string? search,
        int? clientPersonId,
        string? vin,
        DateTime? fromDate,
        DateTime? toDate,
        int? statusId,
        int? mechanicPersonId)
    {
        var query = Context.ServiceOrders
            .AsNoTracking()
            .Include(x => x.Vehicle)
            .ThenInclude(x => x.OwnerHistory)
            .Include(x => x.OrderStatus)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(x =>
                (x.GeneralDescription != null && x.GeneralDescription.Contains(term)) ||
                (x.CancellationReason != null && x.CancellationReason.Contains(term)) ||
                x.Vehicle.Vin.Contains(term) ||
                x.OrderStatus.Name.Contains(term));
        }

        if (clientPersonId.HasValue)
        {
            query = query.Where(x => x.Vehicle.OwnerHistory.Any(owner =>
                owner.PersonId == clientPersonId.Value &&
                owner.EndDate == null));
        }

        if (!string.IsNullOrWhiteSpace(vin))
        {
            var vinTerm = vin.Trim();
            query = query.Where(x => x.Vehicle.Vin.Contains(vinTerm));
        }

        if (fromDate.HasValue)
        {
            query = query.Where(x => x.EntryDate >= fromDate.Value.Date);
        }

        if (toDate.HasValue)
        {
            var exclusiveTo = toDate.Value.Date.AddDays(1);
            query = query.Where(x => x.EntryDate < exclusiveTo);
        }

        if (statusId.HasValue)
        {
            query = query.Where(x => x.OrderStatusId == statusId.Value);
        }

        if (mechanicPersonId.HasValue)
        {
            query = query.Where(x => Context.OrderServices
                .Where(orderService => orderService.ServiceOrderId == x.Id)
                .Any(orderService => Context.MechanicAssignments.Any(assignment =>
                    assignment.OrderServiceId == orderService.Id &&
                    assignment.MechanicPersonId == mechanicPersonId.Value)));
        }

        return query;
    }
}

public sealed class ServiceTypeRepository : GenericRepository<ServiceType>, IServiceTypeRepository
{
    public ServiceTypeRepository(AppDbContext context) : base(context) { }
    public Task<ServiceType?> GetByNameAsync(ServiceTypeName name, CancellationToken ct = default) => FirstByAsync(nameof(ServiceType.Name), name, ct);
    public Task<bool> ExistsNameAsync(ServiceTypeName name, CancellationToken ct = default) => ExistsByAsync(nameof(ServiceType.Name), name, ct);
}

public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }
    public Task<User?> GetByPersonIdAsync(UserPersonId personId, CancellationToken ct = default) => FirstByAsync(nameof(User.PersonId), personId, ct);
    public Task<bool> ExistsPersonIdAsync(UserPersonId personId, CancellationToken ct = default) => ExistsByAsync(nameof(User.PersonId), personId, ct);
}

public sealed class UserRoleRepository : IUserRoleRepository
{
    private readonly AppDbContext _context;

    public UserRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<UserRole?> GetByIdsAsync(UserRoleUserId userId, UserRoleRoleId roleId, CancellationToken ct = default)
    {
        return _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId.Value && x.RoleId == roleId.Value, ct);
    }

    public async Task<IReadOnlyList<UserRole>> GetByUserIdAsync(UserRoleUserId userId, CancellationToken ct = default)
    {
        return await _context.UserRoles.AsNoTracking().Where(x => x.UserId == userId.Value).OrderBy(x => x.RoleId).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<UserRole>> GetByRoleIdAsync(UserRoleRoleId roleId, CancellationToken ct = default)
    {
        return await _context.UserRoles.AsNoTracking().Where(x => x.RoleId == roleId.Value).OrderBy(x => x.UserId).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<UserRole>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.UserRoles.AsNoTracking().OrderBy(x => x.UserId).ThenBy(x => x.RoleId).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<UserRole>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        return await _context.UserRoles.AsNoTracking().OrderBy(x => x.UserId).ThenBy(x => x.RoleId).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
    }

    public Task<int> CountAsync(string? search = null, CancellationToken ct = default)
    {
        return _context.UserRoles.CountAsync(ct);
    }

    public async Task AddAsync(UserRole userRole, CancellationToken ct = default)
    {
        await _context.UserRoles.AddAsync(userRole, ct);
    }

    public Task UpdateAsync(UserRole userRole, CancellationToken ct = default)
    {
        _context.UserRoles.Update(userRole);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(UserRole userRole, CancellationToken ct = default)
    {
        _context.UserRoles.Remove(userRole);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(UserRoleUserId userId, UserRoleRoleId roleId, CancellationToken ct = default)
    {
        return _context.UserRoles.AnyAsync(x => x.UserId == userId.Value && x.RoleId == roleId.Value, ct);
    }
}

public sealed class VehicleBrandRepository : GenericRepository<VehicleBrand>, IVehicleBrandRepository
{
    public VehicleBrandRepository(AppDbContext context) : base(context) { }
    public Task<VehicleBrand?> GetByNameAsync(VehicleBrandName name, CancellationToken ct = default) => FirstByAsync(nameof(VehicleBrand.BrandName), name, ct);
    public Task<bool> ExistsNameAsync(VehicleBrandName name, CancellationToken ct = default) => ExistsByAsync(nameof(VehicleBrand.BrandName), name, ct);
}

public sealed class VehicleModelRepository : GenericRepository<VehicleModel>, IVehicleModelRepository
{
    public VehicleModelRepository(AppDbContext context) : base(context) { }
    public Task<VehicleModel?> GetByBrandAndNameAsync(VehicleModelBrandId brandId, VehicleModelName name, CancellationToken ct = default) => FirstByAsync(nameof(VehicleModel.BrandId), brandId, nameof(VehicleModel.ModelName), name, ct);
    public Task<IReadOnlyList<VehicleModel>> GetByBrandIdAsync(VehicleModelBrandId brandId, CancellationToken ct = default) => ListByAsync(nameof(VehicleModel.BrandId), brandId, ct);
    public Task<bool> ExistsBrandAndNameAsync(VehicleModelBrandId brandId, VehicleModelName name, CancellationToken ct = default) => ExistsByAsync(nameof(VehicleModel.BrandId), brandId, nameof(VehicleModel.ModelName), name, ct);
}
