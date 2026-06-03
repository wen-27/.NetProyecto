namespace Api.DTOs.Auth;

// DTO usado para transportar datos de RegisterClientRequest entre la API y sus consumidores.
public sealed record RegisterClientRequest(
    int DocumentTypeId,
    string DocumentNumber,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? SecondLastName,
    DateOnly? BirthDate,
    int? GenderId,
    int? AddressId,
    string Email,
    string Password,
    int? PhoneCountryId,
    string? PhoneNumber,
    string? AddressText = null);
