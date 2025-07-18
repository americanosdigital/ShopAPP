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
    public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(od => od.Id);

            builder.Property(od => od.Quantity)
                .IsRequired();

            builder.Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(od => od.ProductImageUrl)
                .HasMaxLength(300);

            builder.HasOne(od => od.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(od => od.OrderId) 
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
           
            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
           .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.UpdatedAt);

            builder.Property(u => u.DeletedAt);
        }
    }
}