namespace Domain.Enums;

public enum OrderServiceStatus
{
    Pending = 1,
    Approved = 2,
    InProgress = 3,
    WaitingForParts = 4,
    Completed = 5,
    Rejected = 6,
    Invoiced = 7
}

public enum AdditionalRequestStatus
{
    Draft = 1,
    PendingWorkshopChiefApproval = 2,
    RejectedByWorkshopChief = 3,
    PendingClientApproval = 4,
    RejectedByClient = 5,
    ApprovedByClient = 6,
    AddedToOrder = 7
}

public enum AdditionalRequestType
{
    Service = 1,
    Part = 2,
    ServiceWithParts = 3
}

public enum StockSubmissionStatus
{
    Draft = 1,
    PendingInventoryManagerReview = 2,
    RejectedByInventoryManager = 3,
    ApprovedByInventoryManager = 4,
    AddedToInventory = 5
}

public enum PaymentStatusKind
{
    Pending = 1,
    PendingReceptionVerification = 2,
    Approved = 3,
    Rejected = 4,
    Refunded = 5
}

public enum WorkshopServiceStatus
{
    Active = 1,
    Inactive = 2
}
