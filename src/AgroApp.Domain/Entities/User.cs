using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;
using AgroApp.Domain.Enums;

namespace AgroApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Farmer;
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }

        // Navegación
        public Tenant Tenant { get; set; } = null!;
    }
}