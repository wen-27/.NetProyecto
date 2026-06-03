// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de RegisterClientRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
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
