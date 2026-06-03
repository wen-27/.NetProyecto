namespace Api.DTOs.Suppliers;

// DTO usado para transportar datos de CreateSupplierRequest entre la API y sus consumidores.
public sealed record CreateSupplierRequest(string Name, string? TaxId, string? Phone, string? Email, bool IsActive = true);
// DTO usado para transportar datos de UpdateSupplierRequest entre la API y sus consumidores.
public sealed record UpdateSupplierRequest(string Name, string? TaxId, string? Phone, string? Email, bool IsActive);
// DTO usado para transportar datos de SupplierResponse entre la API y sus consumidores.
public sealed record SupplierResponse(int Id, string Name, string? TaxId, string? Phone, string? Email, bool IsActive);
