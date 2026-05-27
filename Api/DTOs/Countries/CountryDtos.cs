namespace Api.DTOs.Countries;

public sealed record CreateCountryRequest(string Name, string? PhoneCode);
public sealed record UpdateCountryRequest(string Name, string? PhoneCode);
public sealed record CountryResponse(int Id, string Name, string? PhoneCode);
