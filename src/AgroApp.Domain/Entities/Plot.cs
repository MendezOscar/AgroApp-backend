using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class Plot : BaseEntity
    {
        public Guid FarmId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SoilType { get; set; }
        public decimal? AreaHa { get; set; }
        public string? GeoJson { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;

        // Navegación
        public Farm Farm { get; set; } = null!;
        public ICollection<Crop> Crops { get; set; } = new List<Crop>();
        public ICollection<SensorDevice> SensorDevices { get; set; } = new List<SensorDevice>();
        public ICollection<SoilAnalysis> SoilAnalyses { get; set; } = new List<SoilAnalysis>();
    }
}