using Domain.Common;

namespace Domain.Entities;

public class Person : BaseEntity
{
    public string FirstNames { get; set; } = string.Empty;
    public string LastNames { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    
    public ICollection<PersonAddress> Addresses { get; set; } = new List<PersonAddress>();
    public ICollection<PersonDocument> Documents { get; set; } = new List<PersonDocument>();
    public ICollection<PersonEmail> Emails { get; set; } = new List<PersonEmail>();
    public ICollection<PersonPhone> Phones { get; set; } = new List<PersonPhone>();

    public Customer? Customer { get; set; }
    public User? User { get; set; }
}
