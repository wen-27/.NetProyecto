namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IUnitOfWork
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
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
