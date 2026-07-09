using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class FertilizationLog : BaseLog
    {
        public Guid CropId { get; set; }
        public Guid UserId { get; set; }
        public Guid? TaskId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductType { get; set; }
        public decimal? DoseKgHa { get; set; }
        public decimal? TotalKg { get; set; }
        public string? Method { get; set; }
        public decimal? Cost { get; set; }
        public DateTime AppliedAt { get; set; }
        public DateOnly? NextApplication { get; set; }
        public string? Notes { get; set; }

        public Crop Crop { get; set; } = null!;
        public User User { get; set; } = null!;
        public TaskItem? Task { get; set; }
    }
}