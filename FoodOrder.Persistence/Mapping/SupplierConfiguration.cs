
using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Mapping
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
       

        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(e => e.Id)
                       .HasName("pk_supplier_id");

            builder.ToTable("suppliers", "public");

            builder.Property(e => e.Id).HasColumnName("Id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()").IsRequired();

            builder.Property(e => e.IsActive).HasColumnName("IsActive").HasColumnType("boolean");
            builder.Property(e => e.Name).HasColumnName("Name").HasColumnType("character varying").HasMaxLength(100);
            builder.Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();

            builder.Property(e => e.WebUrl).HasColumnName("WebUrl").HasColumnType("character varying").HasMaxLength(500);

        }
    }
}
