// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de VehicleModelDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.VehicleModels;

public sealed record CreateVehicleModelRequest(int BrandId, string ModelName);
public sealed record VehicleModelResponse(int Id, int BrandId, string ModelName);
