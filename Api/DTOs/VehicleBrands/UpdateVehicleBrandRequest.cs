// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdateVehicleBrandRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.VehicleBrands;

public sealed record UpdateVehicleBrandRequest(string BrandName);
