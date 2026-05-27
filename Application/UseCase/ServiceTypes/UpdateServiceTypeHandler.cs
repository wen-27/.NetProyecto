using Application.Abstractions;
using Domain.ValueObjects.ServiceType;
using MediatR;

namespace Application.UseCase.ServiceTypes;

public sealed class UpdateServiceTypeHandler : IRequestHandler<UpdateServiceType>
{
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

        await _serviceTypes.UpdateAsync(serviceType, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
