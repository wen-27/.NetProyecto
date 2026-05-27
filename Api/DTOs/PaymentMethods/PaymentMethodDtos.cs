namespace Api.DTOs.PaymentMethods;

public sealed record CreatePaymentMethodRequest(string Name);
public sealed record UpdatePaymentMethodRequest(string Name);
public sealed record PaymentMethodResponse(int Id, string Name);
