// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de VehicleTypeDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.VehicleTypes;

public sealed record CreateVehicleTypeRequest(string Name);
public sealed record UpdateVehicleTypeRequest(string Name);
public sealed record VehicleTypeResponse(int Id, string Name);
