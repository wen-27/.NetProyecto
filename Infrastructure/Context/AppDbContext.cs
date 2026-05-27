using Domain.Entities;
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
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<EmailDomain> EmailDomains => Set<EmailDomain>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
    public DbSet<OrderPartDetail> OrderPartDetails => Set<OrderPartDetail>();
    public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<PartCategory> PartCategories => Set<PartCategory>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<PersonDocument> PersonDocuments => Set<PersonDocument>();
    public DbSet<PersonEmail> PersonEmails => Set<PersonEmail>();
    public DbSet<PersonPhone> PersonPhones => Set<PersonPhone>();
    public DbSet<PhoneCode> PhoneCodes => Set<PhoneCode>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<ServiceOrder> ServiceOrders => Set<ServiceOrder>();
    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleBrand> VehicleBrands => Set<VehicleBrand>();
    public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();
    public DbSet<VehicleOwnerHistory> VehicleOwnerHistory => Set<VehicleOwnerHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
