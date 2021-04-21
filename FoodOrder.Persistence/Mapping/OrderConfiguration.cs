using System;
using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Mapping
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
       

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id)
                      .HasName("pk_order_id");

            builder.ToTable("Orders", "public");

            builder.Property(e => e.Id).HasColumnName("Id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()");
            builder.Property(e => e.Name).HasColumnName("Name").HasColumnType("character varying").HasMaxLength(100);
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("character varying").HasMaxLength(1000);

            builder.Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedUserId).HasColumnName("CreatedUserId").HasColumnType("uuid");
            builder.Property(e => e.SupplierId).HasColumnName("SupplierId").HasColumnType("uuid").IsRequired().ValueGeneratedNever();
            builder.Property(e => e.ExpireDate).HasColumnName("ExpireDate").HasColumnType("timestamp without time zone").IsRequired();


            builder.HasOne(d => d.CreatedUser)
               .WithMany(p => p.Orders)
               .HasForeignKey(d => d.CreatedUserId)
               .HasConstraintName("fk_user_order_id")
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Supplier)
               .WithMany(p => p.Orders)
               .HasForeignKey(d => d.SupplierId)
               .HasConstraintName("fk_supplier_order_id")
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
