using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class AdditionalServiceRequest : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int MechanicPersonId { get; set; }
    public int? WorkshopChiefPersonId { get; set; }
    public int ClientPersonId { get; set; }
    public AdditionalRequestType RequestType { get; set; }
    public AdditionalRequestStatus Status { get; set; } = AdditionalRequestStatus.PendingWorkshopChiefApproval;
    public int? WorkshopServiceId { get; set; }
    public int? PartId { get; set; }
    public int? Quantity { get; set; }
    public string TechnicalComment { get; set; } = string.Empty;
    public string? WorkshopChiefComment { get; set; }
    public string? ClientComment { get; set; }
    public decimal EstimatedPrice { get; set; }
    public DateTime? WorkshopChiefReviewedAt { get; set; }
    public DateTime? ClientReviewedAt { get; set; }
    public DateTime? AddedToOrderAt { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Person MechanicPerson { get; set; } = null!;
    public Person? WorkshopChiefPerson { get; set; }
    public Person ClientPerson { get; set; } = null!;
    public WorkshopService? WorkshopService { get; set; }
    public Part? Part { get; set; }
}
