namespace Api.DTOs.PaymentStatuses;

public sealed record CreatePaymentStatusRequest(string Name);
public sealed record UpdatePaymentStatusRequest(string Name);
public sealed record PaymentStatusResponse(int Id, string Name);
