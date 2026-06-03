namespace Api.DTOs.PersonPhones;

// DTO usado para transportar datos de CreatePersonPhoneRequest entre la API y sus consumidores.
public sealed record CreatePersonPhoneRequest(int PersonId, int CountryId, string PhoneNumber, bool IsPrimary);
// DTO usado para transportar datos de PersonPhoneResponse entre la API y sus consumidores.
public sealed record PersonPhoneResponse(int Id, int PersonId, int CountryId, string PhoneNumber, bool IsPrimary);
