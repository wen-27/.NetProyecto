using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

public static class ModelBuilderSeeder
{
    public static void SeedCoreData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new { Id = 1, RoleName = "Admin" },
            new { Id = 2, RoleName = "Client" },
            new { Id = 3, RoleName = "Mechanic" },
            new { Id = 4, RoleName = "Receptionist" },
            new { Id = 5, RoleName = "WorkshopChief" },
            new { Id = 6, RoleName = "WarehouseChief" },
            new { Id = 7, RoleName = "InventoryManager" });

        modelBuilder.Entity<OrderStatus>().HasData(
            new { Id = 1, Name = "Created" },
            new { Id = 2, Name = "PendingAssignment" },
            new { Id = 3, Name = "Assigned" },
            new { Id = 4, Name = "InProgress" },
            new { Id = 5, Name = "PendingClientApproval" },
            new { Id = 6, Name = "WaitingForPayment" },
            new { Id = 7, Name = "PaymentUnderReview" },
            new { Id = 8, Name = "Paid" },
            new { Id = 9, Name = "ReadyForDelivery" },
            new { Id = 10, Name = "Delivered" },
            new { Id = 11, Name = "Cancelled" });

        modelBuilder.Entity<MechanicSpecialty>().HasData(
            new { Id = 1, Name = "Engine" },
            new { Id = 2, Name = "Electrical" },
            new { Id = 3, Name = "AirConditioning" },
            new { Id = 4, Name = "Suspension" },
            new { Id = 5, Name = "Brakes" },
            new { Id = 6, Name = "GeneralDiagnostics" },
            new { Id = 7, Name = "Bodywork" },
            new { Id = 8, Name = "Diagnóstico" },
            new { Id = 9, Name = "Mantenimiento" },
            new { Id = 10, Name = "Electricista" },
            new { Id = 11, Name = "Frenos" });

        modelBuilder.Entity<PaymentMethod>().HasData(
            new { Id = 1, Name = "Efectivo" },
            new { Id = 2, Name = "Transferencia" },
            new { Id = 3, Name = "Tarjeta débito" },
            new { Id = 4, Name = "Tarjeta crédito" },
            new { Id = 5, Name = "Nequi" },
            new { Id = 6, Name = "Daviplata" });

        modelBuilder.Entity<PaymentStatus>().HasData(
            new { Id = 1, Name = "Pending" },
            new { Id = 2, Name = "PendingReceptionVerification" },
            new { Id = 3, Name = "Approved" },
            new { Id = 4, Name = "Rejected" },
            new { Id = 5, Name = "Refunded" });

        modelBuilder.Entity<InvoiceStatus>().HasData(
            new { Id = 1, Name = "Draft" },
            new { Id = 2, Name = "Issued" },
            new { Id = 3, Name = "Paid" },
            new { Id = 4, Name = "Cancelled" });

        modelBuilder.Entity<ServiceType>().HasData(
            new { Id = 1, Name = "Preventive Maintenance", EstimatedDays = 1 },
            new { Id = 2, Name = "Mechanical Repair", EstimatedDays = 3 },
            new { Id = 3, Name = "Diagnostics", EstimatedDays = 1 },
            new { Id = 4, Name = "Air Conditioning", EstimatedDays = 2 },
            new { Id = 5, Name = "Electrical", EstimatedDays = 2 },
            new { Id = 6, Name = "Bodywork and Paint", EstimatedDays = 5 });

        modelBuilder.Entity<PartCategory>().HasData(
            new { Id = 1, Name = "Filters" },
            new { Id = 2, Name = "Oils and Lubricants" },
            new { Id = 3, Name = "Brakes" },
            new { Id = 4, Name = "Suspension" },
            new { Id = 5, Name = "Electrical" },
            new { Id = 6, Name = "Air Conditioning" },
            new { Id = 7, Name = "Engine" },
            new { Id = 8, Name = "Bodywork" });

        modelBuilder.Entity<AuditActionType>().HasData(
            new { Id = 1, Name = "CREATE" },
            new { Id = 2, Name = "UPDATE" },
            new { Id = 3, Name = "DELETE" },
            new { Id = 4, Name = "CANCEL" },
            new { Id = 5, Name = "VOID" },
            new { Id = 6, Name = "LOGIN" });

        modelBuilder.Entity<VehicleType>().HasData(
            new { Id = 1, Name = "Sedan" },
            new { Id = 2, Name = "SUV" },
            new { Id = 3, Name = "Pickup" },
            new { Id = 4, Name = "Van" },
            new { Id = 5, Name = "Motorcycle" },
            new { Id = 6, Name = "Truck" });

        modelBuilder.Entity<DocumentType>().HasData(
            new { Id = 1, Code = "CC", Name = "Cedula de Ciudadania" },
            new { Id = 2, Code = "NIT", Name = "NIT" },
            new { Id = 3, Code = "CE", Name = "Cedula de Extranjeria" },
            new { Id = 4, Code = "PAS", Name = "Pasaporte" });

        modelBuilder.Entity<Gender>().HasData(
            new { Id = 1, Name = "Male" },
            new { Id = 2, Name = "Female" },
            new { Id = 3, Name = "Other" },
            new { Id = 4, Name = "PreferNotToSay" });

        modelBuilder.Entity<CardType>().HasData(
            new { Id = 1, Name = "Visa" },
            new { Id = 2, Name = "Mastercard" },
            new { Id = 3, Name = "AmericanExpress" },
            new { Id = 4, Name = "Debit" });

        modelBuilder.Entity<StreetType>().HasData(
            new { Id = 1, Name = "Calle" },
            new { Id = 2, Name = "Carrera" },
            new { Id = 3, Name = "Avenida" },
            new { Id = 4, Name = "Diagonal" },
            new { Id = 5, Name = "Transversal" },
            new { Id = 6, Name = "Circular" });

        modelBuilder.Entity<Country>().HasData(
            new { Id = 1, Name = "Colombia", PhoneCode = "+57" },
            new { Id = 2, Name = "Venezuela", PhoneCode = "+58" },
            new { Id = 3, Name = "Ecuador", PhoneCode = "+593" });

        modelBuilder.Entity<Department>().HasData(
            new { Id = 1, CountryId = 1, Name = "Santander" },
            new { Id = 2, CountryId = 1, Name = "Cundinamarca" },
            new { Id = 3, CountryId = 1, Name = "Antioquia" });

        modelBuilder.Entity<City>().HasData(
            new { Id = 1, DepartmentId = 1, Name = "Bucaramanga" },
            new { Id = 2, DepartmentId = 1, Name = "Floridablanca" },
            new { Id = 3, DepartmentId = 1, Name = "Giron" },
            new { Id = 4, DepartmentId = 2, Name = "Bogota" },
            new { Id = 5, DepartmentId = 3, Name = "Medellin" });
    }
}
