using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class Farm : BaseEntity
    {
        public Guid TenantId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public decimal? AreaHa { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public bool IsActive { get; set; } = true;

        // Navegación
        public Tenant Tenant { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public ICollection<Plot> Plots { get; set; } = new List<Plot>();
    }
}