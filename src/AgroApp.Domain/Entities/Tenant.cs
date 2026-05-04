using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroApp.Domain.Common;

namespace AgroApp.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Plan { get; set; } = "free";
        public bool IsActive { get; set; } = true;

        // Navegación
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Farm> Farms { get; set; } = new List<Farm>();
    }
}