using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.ServiceType;
using MediatR;

namespace Application.UseCase.ServiceTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateServiceType.
public sealed class CreateServiceTypeHandler : IRequestHandler<CreateServiceType, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
