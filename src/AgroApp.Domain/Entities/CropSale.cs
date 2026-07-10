using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities;

public class CropSale : BaseLog
{
    public Guid CropId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly SoldAt { get; set; }
    public decimal QuantityKg { get; set; }
    public decimal PricePerKg { get; set; }
    public string? Buyer { get; set; }
    public string? Notes { get; set; }

    public Crop Crop { get; set; } = null!;
    public User User { get; set; } = null!;
}
