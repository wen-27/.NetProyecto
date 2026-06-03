// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de UserStatus, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
namespace Domain.ValueObjects.User;

public readonly record struct UserStatus
{
    public bool IsActive { get; }

    public UserStatus(bool isActive)
    {
        IsActive = isActive;
    }

    public static UserStatus Active() => new(true);

    public static UserStatus Inactive() => new(false);

    public override string ToString() => IsActive ? "Activo" : "Inactivo";
}
