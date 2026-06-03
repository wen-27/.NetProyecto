// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PersonPhoneDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PersonPhones;

public sealed record CreatePersonPhoneRequest(int PersonId, int CountryId, string PhoneNumber, bool IsPrimary);
public sealed record PersonPhoneResponse(int Id, int PersonId, int CountryId, string PhoneNumber, bool IsPrimary);
