// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de VehicleOwnerHistoryDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.VehicleOwnerHistory;

public sealed record CreateVehicleOwnerHistoryRequest(int VehicleId, int PersonId, DateOnly StartDate);
public sealed record RegisterVehicleOwnerRequest(int VehicleId, int PersonId, DateOnly StartDate);
public sealed record UpdateVehicleOwnerHistoryRequest(int VehicleId, int PersonId, DateOnly StartDate, DateOnly? EndDate);
public sealed record VehicleOwnerHistoryResponse(int Id, int VehicleId, int PersonId, DateOnly StartDate, DateOnly? EndDate);
