using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShopAPP.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public string Document { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public string UserType { get; set; } = "Customer";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; } = DateTime.UtcNow;

        public string? ImageUrl { get; set; }

        public string? ProfileImageUrl { get; set; }
    }

}
