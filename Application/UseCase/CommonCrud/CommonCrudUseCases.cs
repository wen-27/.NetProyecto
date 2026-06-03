using Application.Abstractions;
using Application.Common.Pagination;
using Domain.Common;
using MediatR;

namespace Application.UseCase.CommonCrud;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetEntityById.
public sealed record GetEntityById<TEntity>(int Id) : IRequest<TEntity>
    where TEntity : BaseEntity;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetEntitiesPaged.
public sealed record GetEntitiesPaged<TEntity>(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<TEntity>>
    where TEntity : BaseEntity;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateEntity.
public sealed record CreateEntity<TEntity>(TEntity Entity) : IRequest<int>
    where TEntity : BaseEntity;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateEntity.
public sealed record UpdateEntity<TEntity>(int Id, TEntity Entity) : IRequest
    where TEntity : BaseEntity;

// Caso de uso que modela una accion o consulta de negocio relacionada con DeleteEntity.
public sealed record DeleteEntity<TEntity>(int Id) : IRequest
    where TEntity : BaseEntity;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetEntityById.
public sealed class GetEntityByIdHandler<TEntity> : IRequestHandler<GetEntityById<TEntity>, TEntity>
    where TEntity : BaseEntity
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IGenericRepository<TEntity> _repository;

    public GetEntityByIdHandler(IGenericRepository<TEntity> repository) => _repository = repository;

    public async Task<TEntity> Handle(GetEntityById<TEntity> request, CancellationToken ct)
    {
        return await _repository.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el registro solicitado.");
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetEntitiesPaged.
public sealed class GetEntitiesPagedHandler<TEntity> : IRequestHandler<GetEntitiesPaged<TEntity>, PagedResult<TEntity>>
    where TEntity : BaseEntity
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IGenericRepository<TEntity> _repository;

    public GetEntitiesPagedHandler(IGenericRepository<TEntity> repository) => _repository = repository;

    public async Task<PagedResult<TEntity>> Handle(GetEntitiesPaged<TEntity> request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        var items = await _repository.GetPagedAsync(page, pageSize, request.Search, ct);
        var total = await _repository.CountAsync(request.Search, ct);

        return new PagedResult<TEntity>(items, total, page, pageSize);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateEntity.
public sealed class CreateEntityHandler<TEntity> : IRequestHandler<CreateEntity<TEntity>, int>
    where TEntity : BaseEntity
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEntityHandler(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateEntity<TEntity> request, CancellationToken ct)
    {
        await _repository.AddAsync(request.Entity, ct);
        await _unitOfWork.CommitAsync(ct);
        return request.Entity.Id;
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateEntity.
public sealed class UpdateEntityHandler<TEntity> : IRequestHandler<UpdateEntity<TEntity>>
    where TEntity : BaseEntity
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEntityHandler(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateEntity<TEntity> request, CancellationToken ct)
    {
        _ = await _repository.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el registro solicitado.");

        request.Entity.Id = request.Id;
        await _repository.UpdateAsync(request.Entity, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con DeleteEntity.
public sealed class DeleteEntityHandler<TEntity> : IRequestHandler<DeleteEntity<TEntity>>
    where TEntity : BaseEntity
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEntityHandler(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteEntity<TEntity> request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el registro solicitado.");

        await _repository.RemoveAsync(entity, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
