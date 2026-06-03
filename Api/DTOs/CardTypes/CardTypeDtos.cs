namespace Api.DTOs.CardTypes;

// DTO usado para transportar datos de CreateCardTypeRequest entre la API y sus consumidores.
public sealed record CreateCardTypeRequest(string Name);
// DTO usado para transportar datos de UpdateCardTypeRequest entre la API y sus consumidores.
public sealed record UpdateCardTypeRequest(string Name);
// DTO usado para transportar datos de CardTypeResponse entre la API y sus consumidores.
public sealed record CardTypeResponse(int Id, string Name);
