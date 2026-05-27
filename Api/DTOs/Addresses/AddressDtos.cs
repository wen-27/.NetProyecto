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
