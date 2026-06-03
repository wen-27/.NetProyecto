using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PersonEmail dentro del modelo principal del taller.
public class PersonEmail : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PersonId { get; set; }
    public int EmailDomainId { get; set; }
    public string EmailUser { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public Person Person { get; set; } = null!;
    public EmailDomain EmailDomain { get; set; } = null!;
}
