using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAPP.Infrastructure.Identity.Models;

namespace ShopAPP.Infrastructure.Identity.Mappings
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Document)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(u => u.IsActive)
                   .HasDefaultValue(true);

            builder.Property(u => u.UserType)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasDefaultValue("Customer");

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
