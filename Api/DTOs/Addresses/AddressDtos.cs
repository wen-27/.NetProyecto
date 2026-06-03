// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de AddressDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Addresses;

public sealed record CreateAddressRequest(
    int NeighborhoodId,
    int StreetTypeId,
    string? MainNumber,
    string? SecondaryNumber,
    string? TertiaryNumber,
    string? Complement);

public sealed record UpdateAddressRequest(
    int NeighborhoodId,
    int StreetTypeId,
    string? MainNumber,
    string? SecondaryNumber,
    string? TertiaryNumber,
    string? Complement);

public sealed record AddressResponse(
    int Id,
    int NeighborhoodId,
    int StreetTypeId,
    string? MainNumber,
    string? SecondaryNumber,
    string? TertiaryNumber,
    string? Complement);
