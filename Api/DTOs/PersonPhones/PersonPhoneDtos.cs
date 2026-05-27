namespace Api.DTOs.PersonPhones;

public sealed record CreatePersonPhoneRequest(int PersonId, int CountryId, string PhoneNumber, bool IsPrimary);
public sealed record PersonPhoneResponse(int Id, int PersonId, int CountryId, string PhoneNumber, bool IsPrimary);
