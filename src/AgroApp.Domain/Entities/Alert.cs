using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;
using AgroApp.Domain.Enums;

namespace AgroApp.Domain.Entities
{
    public class Alert : BaseEntity
    {
        public Guid? DeviceId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? PlotId { get; set; }
        public string AlertType { get; set; } = string.Empty;
        public AlertSeverity Severity { get; set; } = AlertSeverity.Warning;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }

        public SensorDevice? Device { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public Plot? Plot { get; set; }
    }
}