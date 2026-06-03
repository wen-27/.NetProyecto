namespace Api.DTOs.PersonPhones;

// DTO usado para transportar datos de UpdatePersonPhoneRequest entre la API y sus consumidores.
public sealed record UpdatePersonPhoneRequest(int CountryId, string PhoneNumber, bool IsPrimary);
