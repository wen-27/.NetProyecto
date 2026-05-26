using System.Collections;
using Domain.Common;

namespace Domain.Entities;

public class Person : BaseEntity
{
    public string FirstName{ get; set;} = string.Empty;
    public string LastName{ get; set;} = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public string? Address { get; set; }
    
    public ICollection<PersonDocument> Documents { get; set; } = new List<PersonDocument>();
    public ICollection<PersonEmail> Emails { get; set; } = new List<PersonEmail>();
    public ICollection<PersonPhone> Phones { get; set; } = new List<PersonPhone>();

    public Customer? Customer { get; set; }
    public User? User { get; set; }

}