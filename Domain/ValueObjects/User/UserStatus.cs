namespace Domain.ValueObjects.User;

// Value Object que encapsula y valida un valor especifico de UserStatus.
public readonly record struct UserStatus
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public bool IsActive { get; }

    public UserStatus(bool isActive)
    {
        IsActive = isActive;
    }

    public static UserStatus Active() => new(true);

    public static UserStatus Inactive() => new(false);

    public override string ToString() => IsActive ? "Activo" : "Inactivo";
}
