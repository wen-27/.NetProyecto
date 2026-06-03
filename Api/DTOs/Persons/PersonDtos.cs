namespace Api.DTOs.Persons;

// DTO usado para transportar datos de CreatePersonRequest entre la API y sus consumidores.
public sealed record CreatePersonRequest(
    int DocumentTypeId,
    string DocumentNumber,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? SecondLastName,
    DateOnly? BirthDate,
    int? GenderId,
    int? AddressId);

// DTO usado para transportar datos de PersonResponse entre la API y sus consumidores.
public sealed record PersonResponse(
    int Id,
    int DocumentTypeId,
    string DocumentNumber,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? SecondLastName,
    DateOnly? BirthDate,
    int? GenderId,
    int? AddressId,
    DateTime CreatedAt);
