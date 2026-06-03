// Responsabilidad: Implementacion de repositorio para persistencia y consultas de VehicleOwnerHistoryRepository; encapsula acceso a DbContext y detalles de EF Core.
// Nota de mantenimiento: Debe evitar reglas de negocio; su responsabilidad principal es consultar y persistir datos.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.VehicleOwnerHistory;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.VehicleOwnerHistory;

public sealed class VehicleOwnerHistoryRepository : IVehicleOwnerHistoryRepository
{
    private readonly AppDbContext _context;

    public VehicleOwnerHistoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Domain.Entities.VehicleOwnerHistory?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return _context.VehicleOwnerHistory.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IReadOnlyList<Domain.Entities.VehicleOwnerHistory>> GetByVehicleIdAsync(VehicleOwnerHistoryVehicleId vehicleId, CancellationToken ct = default)
    {
        return await _context.VehicleOwnerHistory
            .Where(x => x.VehicleId == vehicleId.Value)
            .OrderByDescending(x => x.StartDate)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Domain.Entities.VehicleOwnerHistory>> GetByPersonIdAsync(VehicleOwnerHistoryPersonId personId, CancellationToken ct = default)
    {
        return await _context.VehicleOwnerHistory
            .Where(x => x.PersonId == personId.Value)
            .OrderByDescending(x => x.StartDate)
            .ToListAsync(ct);
    }

    public Task<Domain.Entities.VehicleOwnerHistory?> GetCurrentByVehicleIdAsync(VehicleOwnerHistoryVehicleId vehicleId, CancellationToken ct = default)
    {
        return _context.VehicleOwnerHistory
            .FirstOrDefaultAsync(x => x.VehicleId == vehicleId.Value && x.EndDate == null, ct);
    }

    public async Task<IReadOnlyList<Domain.Entities.VehicleOwnerHistory>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.VehicleOwnerHistory
            .OrderByDescending(x => x.StartDate)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Domain.Entities.VehicleOwnerHistory>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        return await ApplySearch(_context.VehicleOwnerHistory, search)
            .OrderByDescending(x => x.StartDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<int> CountAsync(string? search = null, CancellationToken ct = default)
    {
        return ApplySearch(_context.VehicleOwnerHistory, search).CountAsync(ct);
    }

    public async Task AddAsync(Domain.Entities.VehicleOwnerHistory vehicleOwnerHistory, CancellationToken ct = default)
    {
        await _context.VehicleOwnerHistory.AddAsync(vehicleOwnerHistory, ct);
    }

    public Task UpdateAsync(Domain.Entities.VehicleOwnerHistory vehicleOwnerHistory, CancellationToken ct = default)
    {
        _context.VehicleOwnerHistory.Update(vehicleOwnerHistory);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Domain.Entities.VehicleOwnerHistory vehicleOwnerHistory, CancellationToken ct = default)
    {
        _context.VehicleOwnerHistory.Remove(vehicleOwnerHistory);
        return Task.CompletedTask;
    }

    private static IQueryable<Domain.Entities.VehicleOwnerHistory> ApplySearch(IQueryable<Domain.Entities.VehicleOwnerHistory> query, string? search)
    {
        if (!int.TryParse(search, out var id))
        {
            return query;
        }

        return query.Where(x => x.VehicleId == id || x.PersonId == id);
    }
}
