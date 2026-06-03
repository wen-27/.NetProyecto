// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IUnitOfWork.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
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
