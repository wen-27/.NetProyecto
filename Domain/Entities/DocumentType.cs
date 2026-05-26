using System.Collections;
using Domain.Common;

namespace Domain.Entities;

public class DocumentType : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ICollection<PersonDocument> PersonDocuments { get; set; } = new List<PersonDocument>();
}