using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class IrrigationLog : BaseLog
    {
        public Guid CropId { get; set; }
        public Guid UserId { get; set; }
        public string Method { get; set; } = string.Empty;
        public decimal? VolumeLiters { get; set; }
        public int? DurationMin { get; set; }
        public DateTime AppliedAt { get; set; }
        public string? Notes { get; set; }

        public Crop Crop { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}