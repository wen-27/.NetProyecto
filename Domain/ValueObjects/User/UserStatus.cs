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
