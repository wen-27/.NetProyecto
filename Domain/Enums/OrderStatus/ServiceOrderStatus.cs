namespace Domain.Enums.OrderStatus;

public enum ServiceOrderStatus
{
    Created = 1,
    PendingAssignment = 2,
    Assigned = 3,
    InProgress = 4,
    PendingClientApproval = 5,
    WaitingForPayment = 6,
    PaymentUnderReview = 7,
    Paid = 8,
    ReadyForDelivery = 9,
    Delivered = 10,
    Cancelled = 11,

    Pending = Created,
    Completed = ReadyForDelivery
}
