namespace Api.DTOs.Genders;

public sealed record CreateGenderRequest(string Name);
public sealed record UpdateGenderRequest(string Name);
public sealed record GenderResponse(int Id, string Name);
