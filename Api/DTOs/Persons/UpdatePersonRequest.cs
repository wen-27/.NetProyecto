namespace Api.DTOs.Persons;

public sealed record UpdatePersonRequest(
    int DocumentTypeId,
    string DocumentNumber,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? SecondLastName,
    DateOnly? BirthDate,
    int? GenderId,
    int? AddressId)
{
    public string FirstNames => string.Join(' ', new[] { FirstName, MiddleName }.Where(x => !string.IsNullOrWhiteSpace(x)));
    public string LastNames => string.Join(' ', new[] { LastName, SecondLastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
}
