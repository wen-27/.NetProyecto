namespace Api.DTOs.Users;

// DTO usado para transportar datos de ChangeUserStatusRequest entre la API y sus consumidores.
public sealed record ChangeUserStatusRequest(bool Status);
