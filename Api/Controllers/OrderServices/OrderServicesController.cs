using Api.DTOs.OrderServices;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.OrderServices;

public sealed class OrderServicesController : CrudController<OrderService, CreateOrderServiceRequest, UpdateOrderServiceRequest, OrderServiceResponse>
{
    public OrderServicesController(ISender sender) : base(sender) { }
}
