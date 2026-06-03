namespace Api.DTOs.Auth;

// DTO usado para transportar datos de LoginRequest entre la API y sus consumidores.
public sealed record LoginRequest(string Email, string Password);
