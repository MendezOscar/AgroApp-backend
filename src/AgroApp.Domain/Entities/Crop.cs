using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;
using AgroApp.Domain.Enums;

namespace AgroApp.Domain.Entities
{
    public class Crop : BaseEntity
    {
        public Guid PlotId { get; set; }
        public string CropType { get; set; } = string.Empty;
        public string? Variety { get; set; }
        public DateOnly PlantedAt { get; set; }
        public DateOnly? EstimatedHarvest { get; set; }
        public DateOnly? HarvestedAt { get; set; }
        public CropStatus Status { get; set; } = CropStatus.Active;
        public decimal? YieldKg { get; set; }
        public string? Notes { get; set; }

        // Navegación
        public Plot Plot { get; set; } = null!;
        public ICollection<PhenologyStage> PhenologyStages { get; set; } = [];
        public ICollection<IrrigationLog> IrrigationLogs { get; set; } = [];
        public ICollection<FertilizationLog> FertilizationLogs { get; set; } = [];
        public ICollection<LaborLog> LaborLogs { get; set; } = [];
        public ICollection<CropImage> CropImages { get; set; } = [];
        public ICollection<CropSale> CropSales { get; set; } = [];
    }
}