namespace Api.DTOs.Countries;

// DTO usado para transportar datos de CreateCountryRequest entre la API y sus consumidores.
public sealed record CreateCountryRequest(string Name, string? PhoneCode);
// DTO usado para transportar datos de UpdateCountryRequest entre la API y sus consumidores.
public sealed record UpdateCountryRequest(string Name, string? PhoneCode);
// DTO usado para transportar datos de CountryResponse entre la API y sus consumidores.
public sealed record CountryResponse(int Id, string Name, string? PhoneCode);
