namespace Api.DTOs.Auth;

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
