// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de SupplierDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Suppliers;

public sealed record CreateSupplierRequest(string Name, string? TaxId, string? Phone, string? Email, bool IsActive = true);
public sealed record UpdateSupplierRequest(string Name, string? TaxId, string? Phone, string? Email, bool IsActive);
public sealed record SupplierResponse(int Id, string Name, string? TaxId, string? Phone, string? Email, bool IsActive);
