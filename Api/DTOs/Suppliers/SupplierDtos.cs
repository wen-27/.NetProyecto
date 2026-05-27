namespace Api.DTOs.Suppliers;

public sealed record CreateSupplierRequest(string Name, string? TaxId, string? Phone, string? Email, bool IsActive = true);
public sealed record UpdateSupplierRequest(string Name, string? TaxId, string? Phone, string? Email, bool IsActive);
public sealed record SupplierResponse(int Id, string Name, string? TaxId, string? Phone, string? Email, bool IsActive);
