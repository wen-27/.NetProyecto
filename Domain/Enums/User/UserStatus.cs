namespace Domain.Enums.User;

// Enumeracion que limita los valores permitidos para UserStatus.
public enum UserStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Inactive = 0,
    Active = 1
}
