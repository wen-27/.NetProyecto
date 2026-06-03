using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

// Entidad de dominio que representa WorkshopService dentro del modelo principal del taller.
public class WorkshopService : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    // Nombre comercial del servicio que vera el usuario en catalogos y ordenes.
    public string Name { get; set; } = string.Empty;

    // Explica en lenguaje operativo que incluye el servicio del taller.
    public string Description { get; set; } = string.Empty;

    // Agrupa servicios similares para filtros, reportes o navegacion.
    public string Category { get; set; } = string.Empty;

    // Porcentaje de mano de obra aplicado sobre el subtotal de repuestos.
    public decimal LaborPercentage { get; set; }

    // Total acumulado de los repuestos asociados a este servicio preconfigurado.
    public decimal PartsSubtotal { get; set; }

    // Valor monetario calculado para la mano de obra.
    public decimal LaborAmount { get; set; }

    // Precio final que se usa al agregar el servicio a una orden o al facturarlo.
    public decimal FinalPrice { get; set; }

    // Permite desactivar el servicio sin borrar su historico en ordenes anteriores.
    public WorkshopServiceStatus Status { get; set; } = WorkshopServiceStatus.Active;

    // Repuestos base que componen este servicio del catalogo.
    public ICollection<WorkshopServicePart> Parts { get; set; } = new List<WorkshopServicePart>();

    // Ordenes reales donde este servicio fue seleccionado o agregado.
    public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    // Solicitudes creadas por mecanicos o jefes para agregar este servicio a una orden existente.
    public ICollection<AdditionalServiceRequest> AdditionalServiceRequests { get; set; } = new List<AdditionalServiceRequest>();
}
