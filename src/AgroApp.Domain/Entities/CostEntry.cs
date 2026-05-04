using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class CostEntry : BaseLog
    {
        public Guid CropId { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateOnly EntryDate { get; set; }

        public Crop Crop { get; set; } = null!;
    }
}