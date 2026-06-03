// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de CountryDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Countries;

public sealed record CreateCountryRequest(string Name, string? PhoneCode);
public sealed record UpdateCountryRequest(string Name, string? PhoneCode);
public sealed record CountryResponse(int Id, string Name, string? PhoneCode);
