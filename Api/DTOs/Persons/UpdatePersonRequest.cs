// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdatePersonRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
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
