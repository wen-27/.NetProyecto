namespace Application.Common.Pagination;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed record PagedResult<T>
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public PagedResult(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount < 0 ? 0 : totalCount;
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 1 : pageSize;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public static PagedResult<T> Empty(int page, int pageSize) => new(Array.Empty<T>(), 0, page, pageSize);
}
