using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa EmailDomain dentro del modelo principal del taller.
public class EmailDomain : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Domain { get; set; } = string.Empty;
    public ICollection<PersonEmail> PersonEmails { get; set; } = new List<PersonEmail>();
}
