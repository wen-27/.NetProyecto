using Domain.Entities;
using Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Audit> Audits => Set<Audit>();
    public DbSet<AuditActionType> AuditActionTypes => Set<AuditActionType>();
    public DbSet<AdditionalServiceRequest> AdditionalServiceRequests => Set<AdditionalServiceRequest>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<CardType> CardTypes => Set<CardType>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<EmailDomain> EmailDomains => Set<EmailDomain>();
    public DbSet<Gender> Genders => Set<Gender>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
    public DbSet<InvoiceStatus> InvoiceStatuses => Set<InvoiceStatus>();
    public DbSet<InventoryHistory> InventoryHistory => Set<InventoryHistory>();
    public DbSet<MechanicAssignment> MechanicAssignments => Set<MechanicAssignment>();
    public DbSet<MechanicDiagnostic> MechanicDiagnostics => Set<MechanicDiagnostic>();
    public DbSet<MechanicSpecialty> MechanicSpecialties => Set<MechanicSpecialty>();
    public DbSet<MechanicSpecialtyAssignment> MechanicSpecialtyAssignments => Set<MechanicSpecialtyAssignment>();
    public DbSet<Neighborhood> Neighborhoods => Set<Neighborhood>();
    public DbSet<OrderService> OrderServices => Set<OrderService>();
    public DbSet<OrderServicePart> OrderServiceParts => Set<OrderServicePart>();
    public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();
    public DbSet<OrderStatusHistory> OrderStatusHistory => Set<OrderStatusHistory>();
    public DbSet<PartBrand> PartBrands => Set<PartBrand>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<PartCategory> PartCategories => Set<PartCategory>();
    public DbSet<PartPurchase> PartPurchases => Set<PartPurchase>();
    public DbSet<PartPurchaseDetail> PartPurchaseDetails => Set<PartPurchaseDetail>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentCard> PaymentCards => Set<PaymentCard>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
    public DbSet<PaymentStatus> PaymentStatuses => Set<PaymentStatus>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<PersonEmail> PersonEmails => Set<PersonEmail>();
    public DbSet<PersonPhone> PersonPhones => Set<PersonPhone>();
    public DbSet<PersonRole> PersonRoles => Set<PersonRole>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<ServiceOrder> ServiceOrders => Set<ServiceOrder>();
    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    public DbSet<StreetType> StreetTypes => Set<StreetType>();
    public DbSet<StockSubmission> StockSubmissions => Set<StockSubmission>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleBrand> VehicleBrands => Set<VehicleBrand>();
    public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();
    public DbSet<VehicleEntryInventory> VehicleEntryInventory => Set<VehicleEntryInventory>();
    public DbSet<VehicleOwnerHistory> VehicleOwnerHistory => Set<VehicleOwnerHistory>();
    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<WorkshopService> WorkshopServices => Set<WorkshopService>();
    public DbSet<WorkshopServicePart> WorkshopServiceParts => Set<WorkshopServicePart>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.SeedCoreData();
    }
}
