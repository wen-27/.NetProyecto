namespace Application.Abstractions;

public interface IUnitOfWork
{
    IPersonRepository Persons { get; }
    IVehicleRepository Vehicles { get; }
    IVehicleOwnerHistoryRepository VehicleOwnerHistory { get; }
    IServiceOrderRepository ServiceOrders { get; }
    IOrderStatusHistoryRepository OrderStatusHistory { get; }
    IOrderServiceRepository OrderServices { get; }
    IOrderServicePartRepository OrderServiceParts { get; }
    IPartRepository Parts { get; }
    IInvoiceRepository Invoices { get; }
    IInvoiceDetailRepository InvoiceDetails { get; }
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IUserRoleRepository UserRoles { get; }
    IAuditRepository Audits { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
