namespace Application.Common.Pagination;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public sealed record PaginationRequest(int Page = 1, int PageSize = 10, string? Search = null)
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public int Page { get; } = Page < 1 ? 1 : Page;

    public int PageSize { get; } = PageSize switch
    {
        < 1 => 10,
        > 100 => 100,
        _ => PageSize
    };

    public string? Search { get; } = string.IsNullOrWhiteSpace(Search) ? null : Search.Trim();

    public int Skip => (Page - 1) * PageSize;
}
