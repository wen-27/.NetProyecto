using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Vehicle;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Vehicles;

// Repositorio que encapsula consultas y persistencia de datos usando EF Core.
public sealed class VehicleRepository : IVehicleRepository
{
    // Los metodos de repositorio deben enfocarse en acceso a datos y evitar reglas de negocio.
    private readonly AppDbContext _context;

    public VehicleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Vehicle?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public Task<Vehicle?> GetByPlateAsync(VehiclePlate plate, CancellationToken ct = default)
    {
        return _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == plate.Value, ct);
    }

    public Task<Vehicle?> GetByVinAsync(VehicleVin vin, CancellationToken ct = default)
    {
        return _context.Vehicles.FirstOrDefaultAsync(x => x.Vin == vin.Value, ct);
    }

    public async Task<IReadOnlyList<Vehicle>> GetByModelIdAsync(VehicleModelId modelId, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Where(x => x.ModelId == modelId.Value)
            .OrderBy(x => x.Id)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Vehicles
            .OrderBy(x => x.Id)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Vehicle>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        return await ApplySearch(_context.Vehicles, search)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<int> CountAsync(string? search = null, CancellationToken ct = default)
    {
        return ApplySearch(_context.Vehicles, search).CountAsync(ct);
    }

    public async Task<IReadOnlyList<Vehicle>> GetFilteredAsync(
        int page,
        int pageSize,
        string? search = null,
        string? vin = null,
        int? clientPersonId = null,
        CancellationToken ct = default)
    {
        return await ApplyFilters(_context.Vehicles.AsNoTracking(), search, vin, clientPersonId)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<int> CountFilteredAsync(
        string? search = null,
        string? vin = null,
        int? clientPersonId = null,
        CancellationToken ct = default)
    {
        return ApplyFilters(_context.Vehicles.AsNoTracking(), search, vin, clientPersonId).CountAsync(ct);
    }

    public async Task AddAsync(Vehicle vehicle, CancellationToken ct = default)
    {
        await _context.Vehicles.AddAsync(vehicle, ct);
    }

    public Task UpdateAsync(Vehicle vehicle, CancellationToken ct = default)
    {
        _context.Vehicles.Update(vehicle);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Vehicle vehicle, CancellationToken ct = default)
    {
        _context.Vehicles.Remove(vehicle);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsVinAsync(VehicleVin vin, CancellationToken ct = default)
    {
        return _context.Vehicles.AnyAsync(x => x.Vin == vin.Value, ct);
    }

    public Task<bool> ExistsPlateAsync(VehiclePlate plate, CancellationToken ct = default)
    {
        return _context.Vehicles.AnyAsync(x => x.Plate == plate.Value, ct);
    }

    private static IQueryable<Vehicle> ApplySearch(IQueryable<Vehicle> query, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        var term = search.Trim();
        return query.Where(x => x.Plate.Contains(term) || x.Vin.Contains(term) || (x.Color != null && x.Color.Contains(term)));
    }

    private static IQueryable<Vehicle> ApplyFilters(IQueryable<Vehicle> query, string? search, string? vin, int? clientPersonId)
    {
        query = ApplySearch(query, search);

        if (!string.IsNullOrWhiteSpace(vin))
        {
            var vinTerm = vin.Trim();
            query = query.Where(x => x.Plate.Contains(vinTerm) || x.Vin.Contains(vinTerm));
        }

        if (clientPersonId.HasValue)
        {
            query = query.Where(x => x.OwnerHistory.Any(owner =>
                owner.PersonId == clientPersonId.Value &&
                owner.EndDate == null));
        }

        return query;
    }
}
