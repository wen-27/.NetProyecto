namespace Domain.ValueObjects.Customer;

public readonly record struct CustomerStatus
{
    public bool IsActive { get; }

    public CustomerStatus(bool isActive)
    {
        IsActive = isActive;
    }

    public static CustomerStatus Active() => new(true);

    public static CustomerStatus Inactive() => new(false);

    public override string ToString() => IsActive ? "Activo" : "Inactivo";
}
