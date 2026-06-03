namespace Domain.Common;

// Tipo comun del dominio reutilizado por entidades u objetos de valor.
public abstract class BaseEntity
{
    // El contenido de este tipo se mantiene agrupado alrededor de una unica responsabilidad.
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

}