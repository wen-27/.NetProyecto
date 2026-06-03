// Responsabilidad: Entidad de dominio Part; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Part : BaseEntity
{
    public int PartCategoryId { get; set; }
    public int? PartBrandId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int MinimumStock { get; set; }
    public decimal UnitPrice { get; set; }

    public PartCategory PartCategory { get; set; } = null!;
    public PartBrand? PartBrand { get; set; }
    public ICollection<OrderServicePart> OrderServiceParts { get; set; } = new List<OrderServicePart>();
    public ICollection<WorkshopServicePart> WorkshopServiceParts { get; set; } = new List<WorkshopServicePart>();
    public ICollection<AdditionalServiceRequest> AdditionalServiceRequests { get; set; } = new List<AdditionalServiceRequest>();
    public ICollection<InventoryHistory> InventoryHistory { get; set; } = new List<InventoryHistory>();
    public ICollection<PartPurchaseDetail> PurchaseDetails { get; set; } = new List<PartPurchaseDetail>();
}
