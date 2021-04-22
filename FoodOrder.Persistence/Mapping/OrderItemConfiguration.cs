using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Mapping
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.Id)
                   .HasName("pk_orderItem_id");

            builder.ToTable("order_items", "public");

            builder.Property(e => e.Id).HasColumnName("Id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()");
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("character varying").HasMaxLength(1000);
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()");
            builder.Property(e => e.CreatedUserId).HasColumnName("CreatedUserId").HasColumnType("uuid");
            builder.Property(e => e.OrderId).HasColumnName("OrderId").HasColumnType("uuid");


            builder.HasOne(d => d.Order)
               .WithMany(p => p.OrderItems)
               .HasForeignKey(d => d.OrderId)
               .HasConstraintName("fk_orderitems_order_id")
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.CreatedUser)
               .WithMany(p => p.CreatedOrderItems)
               .HasForeignKey(d => d.CreatedUserId)
               .HasConstraintName("fk_orderitems_user_id")
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}