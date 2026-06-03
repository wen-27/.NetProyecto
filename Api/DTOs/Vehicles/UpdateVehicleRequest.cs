// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdateVehicleRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Vehicles;

public sealed record UpdateVehicleRequest(
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true);
