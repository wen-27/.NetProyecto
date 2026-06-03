// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateServiceTypeHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.ServiceType;
using MediatR;

namespace Application.UseCase.ServiceTypes;

public sealed class CreateServiceTypeHandler : IRequestHandler<CreateServiceType, int>
{
    private readonly IServiceTypeRepository _serviceTypes;
    private readonly IUnitOfWork _unitOfWork;

    public CreateServiceTypeHandler(IServiceTypeRepository serviceTypes, IUnitOfWork unitOfWork)
    {
        _serviceTypes = serviceTypes;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateServiceType request, CancellationToken ct)
    {
        var name = new ServiceTypeName(request.Name);

        if (await _serviceTypes.ExistsNameAsync(name, ct))
        {
            throw new InvalidOperationException("Ya existe un tipo de servicio con ese nombre.");
        }

        var serviceType = new ServiceType
        {
            Name = name.Value,
            EstimatedDays = request.EstimatedDays < 1 ? 1 : request.EstimatedDays
        };

        await _serviceTypes.AddAsync(serviceType, ct);
        await _unitOfWork.CommitAsync(ct);

        return serviceType.Id;
    }
}
