// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PaymentMethodDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PaymentMethods;

public sealed record CreatePaymentMethodRequest(string Name);
public sealed record UpdatePaymentMethodRequest(string Name);
public sealed record PaymentMethodResponse(int Id, string Name);
