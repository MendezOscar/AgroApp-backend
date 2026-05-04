using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class CropImage : BaseEntity
    {
        public Guid CropId { get; set; }
        public Guid UserId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string StorageKey { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? AiDiagnosis { get; set; }
        public decimal? AiConfidence { get; set; }
        public DateTime? TakenAt { get; set; }

        public Crop Crop { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}