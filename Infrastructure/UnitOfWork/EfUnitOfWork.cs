using Application.Abstractions;
using Infrastructure.Context;

namespace Infrastructure.UnitOfWork;

// Unit of Work que coordina repositorios y confirma cambios como una unidad logica.
public class EfUnitOfWork : IUnitOfWork
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    private readonly AppDbContext _dbContext;

    public EfUnitOfWork(
        AppDbContext dbContext,
        IPersonRepository persons,
        IVehicleRepository vehicles,
        IVehicleOwnerHistoryRepository vehicleOwnerHistory,
        IServiceOrderRepository serviceOrders,
        IOrderStatusHistoryRepository orderStatusHistory,
        IOrderServiceRepository orderServices,
        IOrderServicePartRepository orderServiceParts,
        IPartRepository parts,
        IInvoiceRepository invoices,
        IInvoiceDetailRepository invoiceDetails,
        IUserRepository users,
        IRoleRepository roles,
        IUserRoleRepository userRoles,
        IAuditRepository audits)
    {
        _dbContext = dbContext;
        Persons = persons;
        Vehicles = vehicles;
        VehicleOwnerHistory = vehicleOwnerHistory;
        ServiceOrders = serviceOrders;
        OrderStatusHistory = orderStatusHistory;
        OrderServices = orderServices;
        OrderServiceParts = orderServiceParts;
        Parts = parts;
        Invoices = invoices;
        InvoiceDetails = invoiceDetails;
        Users = users;
        Roles = roles;
        UserRoles = userRoles;
        Audits = audits;
    }

    public IPersonRepository Persons { get; }
    public IVehicleRepository Vehicles { get; }
    public IVehicleOwnerHistoryRepository VehicleOwnerHistory { get; }
    public IServiceOrderRepository ServiceOrders { get; }
    public IOrderStatusHistoryRepository OrderStatusHistory { get; }
    public IOrderServiceRepository OrderServices { get; }
    public IOrderServicePartRepository OrderServiceParts { get; }
    public IPartRepository Parts { get; }
    public IInvoiceRepository Invoices { get; }
    public IInvoiceDetailRepository InvoiceDetails { get; }
    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }
    public IUserRoleRepository UserRoles { get; }
    public IAuditRepository Audits { get; }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
