namespace Api.DTOs.MechanicSpecialties;

public sealed record CreateMechanicSpecialtyRequest(string Name);
public sealed record UpdateMechanicSpecialtyRequest(string Name);
public sealed record MechanicSpecialtyResponse(int Id, string Name);
