namespace Api.DTOs.ServiceOrders;

// DTO usado para transportar datos de RecordServiceOrderWorkRequest entre la API y sus consumidores.
public sealed record RecordServiceOrderWorkRequest(string WorkPerformed);
