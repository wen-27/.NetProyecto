namespace Api.DTOs.CardTypes;

public sealed record CreateCardTypeRequest(string Name);
public sealed record UpdateCardTypeRequest(string Name);
public sealed record CardTypeResponse(int Id, string Name);
