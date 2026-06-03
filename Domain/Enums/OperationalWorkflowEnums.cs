namespace Domain.Enums;

// Enumeracion que limita los valores permitidos para OrderServiceStatus.
public enum OrderServiceStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Pending = 1,
    Approved = 2,
    InProgress = 3,
    WaitingForParts = 4,
    Completed = 5,
    Rejected = 6,
    Invoiced = 7
}

// Enumeracion que limita los valores permitidos para AdditionalRequestStatus.
public enum AdditionalRequestStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Draft = 1,
    PendingWorkshopChiefApproval = 2,
    RejectedByWorkshopChief = 3,
    PendingClientApproval = 4,
    RejectedByClient = 5,
    ApprovedByClient = 6,
    AddedToOrder = 7
}

// Enumeracion que limita los valores permitidos para AdditionalRequestType.
public enum AdditionalRequestType
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Service = 1,
    Part = 2,
    ServiceWithParts = 3
}

// Enumeracion que limita los valores permitidos para StockSubmissionStatus.
public enum StockSubmissionStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Draft = 1,
    PendingInventoryManagerReview = 2,
    RejectedByInventoryManager = 3,
    ApprovedByInventoryManager = 4,
    AddedToInventory = 5
}

// Enumeracion que limita los valores permitidos para PaymentStatusKind.
public enum PaymentStatusKind
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Pending = 1,
    PendingReceptionVerification = 2,
    Approved = 3,
    Rejected = 4,
    Refunded = 5
}

// Enumeracion que limita los valores permitidos para WorkshopServiceStatus.
public enum WorkshopServiceStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    Active = 1,
    Inactive = 2
}

// Enumeracion que limita los valores permitidos para MechanicDiagnosticStatus.
public enum MechanicDiagnosticStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
    PendingWorkshopChiefApproval = 1,
    Approved = 2,
    Rejected = 3
}
