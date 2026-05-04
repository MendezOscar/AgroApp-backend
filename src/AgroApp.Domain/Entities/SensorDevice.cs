using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class SensorDevice : BaseEntity
    {
        public Guid PlotId { get; set; }
        public string DeviceCode { get; set; } = string.Empty;
        public string DeviceType { get; set; } = "multi";
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public int? BatteryPct { get; set; }
        public string? FirmwareVer { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastSeenAt { get; set; }

        public Plot Plot { get; set; } = null!;
        public ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    }
}