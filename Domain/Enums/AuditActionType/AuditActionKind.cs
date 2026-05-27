namespace Domain.Enums.AuditActionType;

public enum AuditActionKind
{
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
