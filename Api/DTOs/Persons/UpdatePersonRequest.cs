namespace Api.DTOs.Persons;

// DTO usado para transportar datos de UpdatePersonRequest entre la API y sus consumidores.
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
    // Estas propiedades forman el contrato publico o interno que se serializa entre capas.
    public string FirstNames => string.Join(' ', new[] { FirstName, MiddleName }.Where(x => !string.IsNullOrWhiteSpace(x)));
    public string LastNames => string.Join(' ', new[] { LastName, SecondLastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
}
