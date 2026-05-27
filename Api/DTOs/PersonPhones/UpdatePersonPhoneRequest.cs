namespace Api.DTOs.PersonPhones;

public sealed record UpdatePersonPhoneRequest(int PhoneCodeId, string PhoneNumber, bool IsPrimary);
