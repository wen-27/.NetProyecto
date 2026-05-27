using Domain.Common;

namespace Domain.Entities;

public class Person : BaseEntity
{
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? SecondLastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public int? GenderId { get; set; }
    public int? AddressId { get; set; }

    public string FirstNames
    {
        get => string.Join(' ', new[] { FirstName, MiddleName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        set => FirstName = value;
    }

    public string LastNames
    {
        get => string.Join(' ', new[] { LastName, SecondLastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        set => LastName = value;
    }

    public DateTime RegistrationDate
    {
        get => CreatedAt;
        set => CreatedAt = value;
    }
    
    public DocumentType DocumentType { get; set; } = null!;
    public Gender? Gender { get; set; }
    public Address? Address { get; set; }
    public ICollection<PersonEmail> Emails { get; set; } = new List<PersonEmail>();
    public ICollection<PersonPhone> Phones { get; set; } = new List<PersonPhone>();
    public ICollection<PersonRole> PersonRoles { get; set; } = new List<PersonRole>();
    public ICollection<VehicleOwnerHistory> VehicleHistory { get; set; } = new List<VehicleOwnerHistory>();

    public User? User { get; set; }
}
