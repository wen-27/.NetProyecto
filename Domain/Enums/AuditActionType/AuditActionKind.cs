namespace Domain.Enums.AuditActionType;

// Enumeracion que limita los valores permitidos para AuditActionKind.
public enum AuditActionKind
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Create = 1,
    Update = 2,
    Delete = 3,
    Login = 4,
    Logout = 5,
    GenerateInvoice = 6,
    ChangeStatus = 7,
    AssignPart = 8,
    AdjustStock = 9
}
