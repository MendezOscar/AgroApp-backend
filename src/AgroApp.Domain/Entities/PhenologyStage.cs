using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class PhenologyStage : BaseLog
    {
        public Guid CropId { get; set; }
        public string StageName { get; set; } = string.Empty;
        public DateOnly StartedAt { get; set; }
        public DateOnly? EndedAt { get; set; }
        public string? Notes { get; set; }

        public Crop Crop { get; set; } = null!;
    }
}