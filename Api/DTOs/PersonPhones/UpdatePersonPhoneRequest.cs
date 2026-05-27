namespace Api.DTOs.PersonPhones;

public sealed record UpdatePersonPhoneRequest(int CountryId, string PhoneNumber, bool IsPrimary);
