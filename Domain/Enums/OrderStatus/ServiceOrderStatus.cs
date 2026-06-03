namespace Domain.Enums.OrderStatus;

// Enumeracion que limita los valores permitidos para ServiceOrderStatus.
public enum ServiceOrderStatus
{
    // Cada valor enum representa una opcion valida que otras capas pueden comparar sin usar numeros o textos sueltos.
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
