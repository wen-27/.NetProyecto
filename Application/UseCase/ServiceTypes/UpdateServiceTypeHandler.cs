using Application.Abstractions;
using Domain.ValueObjects.ServiceType;
using MediatR;

namespace Application.UseCase.ServiceTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateServiceType.
public sealed class UpdateServiceTypeHandler : IRequestHandler<UpdateServiceType>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IServiceTypeRepository _serviceTypes;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServiceTypeHandler(IServiceTypeRepository serviceTypes, IUnitOfWork unitOfWork)
    {
        _serviceTypes = serviceTypes;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateServiceType request, CancellationToken ct)
    {
        var serviceType = await _serviceTypes.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el tipo de servicio.");

        var name = new ServiceTypeName(request.Name);

        serviceType.Name = name.Value;
        serviceType.EstimatedDays = request.EstimatedDays < 1 ? 1 : request.EstimatedDays;

        await _serviceTypes.UpdateAsync(serviceType, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
