using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroApp.Domain.Entities
{
    public class SensorReading
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DeviceId { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? HumidityAir { get; set; }
        public decimal? HumiditySoil { get; set; }
        public decimal? Luminosity { get; set; }
        public decimal? RainMm { get; set; }
        public decimal? Ph { get; set; }
        public decimal? Ec { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        public SensorDevice Device { get; set; } = null!;
    }
}