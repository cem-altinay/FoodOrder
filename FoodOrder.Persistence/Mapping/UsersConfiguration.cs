using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Mapping
{
    public class UsersConfiguration: IEntityTypeConfiguration<Users>
    {
       

        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users", "public");
            builder.HasKey(i => i.Id).HasName("pk_user_id");
            builder.Property(i => i.Id).HasColumnName("Id")
                                       .HasColumnType("uuid")
                                       .HasDefaultValueSql("UUID_GENERATE_V4()")
                                       .IsRequired();

            builder.Property(i => i.FirstName).HasColumnName("FirstName")
                                             .HasColumnType("character varying")
                                             .HasMaxLength(100);

            builder.Property(i => i.LastName).HasColumnName("LastName")
                                         .HasColumnType("character varying")
                                         .HasMaxLength(100);

            builder.Property(i => i.Email).HasColumnName("Email")
                                       .HasColumnType("character varying")
                                       .HasMaxLength(100);

            builder.Property(i => i.Password).HasColumnName("Password").HasColumnType("character varying").HasMaxLength(250);

            builder.Property(i => i.CreateDate).HasColumnName("CreateDate")
                                               .HasColumnType("timestamp without time zone")
                                               .HasDefaultValueSql("NOW()");

            builder.Property(i => i.IsActive).HasColumnName("IsActive");
                                        
        }
    }
}
