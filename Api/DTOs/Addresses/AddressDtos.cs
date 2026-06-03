namespace Api.DTOs.Addresses;

// DTO usado para transportar datos de CreateAddressRequest entre la API y sus consumidores.
public sealed record CreateAddressRequest(
    int NeighborhoodId,
    int StreetTypeId,
    string? MainNumber,
    string? SecondaryNumber,
    string? TertiaryNumber,
    string? Complement);

// DTO usado para transportar datos de UpdateAddressRequest entre la API y sus consumidores.
public sealed record UpdateAddressRequest(
    int NeighborhoodId,
    int StreetTypeId,
    string? MainNumber,
    string? SecondaryNumber,
    string? TertiaryNumber,
    string? Complement);

// DTO usado para transportar datos de AddressResponse entre la API y sus consumidores.
public sealed record AddressResponse(
    int Id,
    int NeighborhoodId,
    int StreetTypeId,
    string? MainNumber,
    string? SecondaryNumber,
    string? TertiaryNumber,
    string? Complement);
