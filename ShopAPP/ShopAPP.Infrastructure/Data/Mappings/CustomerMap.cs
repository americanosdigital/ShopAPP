using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Infrastructure.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .HasMaxLength(35);

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(300);

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
           .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.UpdatedAt);

            builder.Property(u => u.DeletedAt);

            builder.Property(u => u.ImageUrl)
         .HasMaxLength(300);
        }
    }
}