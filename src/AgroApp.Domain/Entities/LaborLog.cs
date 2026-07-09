using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class LaborLog : BaseLog
    {
        public Guid CropId { get; set; }
        public Guid UserId { get; set; }
        public Guid? TaskId { get; set; }
        public string ActivityType { get; set; } = string.Empty;
        public decimal? HoursWorked { get; set; }
        public int WorkersCount { get; set; } = 1;
        public decimal? Cost { get; set; }
        public DateTime PerformedAt { get; set; }
        public string? Notes { get; set; }

        public Crop Crop { get; set; } = null!;
        public User User { get; set; } = null!;
        public TaskItem? Task { get; set; }
    }
}