using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(65)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(x => x.Email, "IX_Customer_Email")
                .IsUnique();

            builder.HasMany(x => x.Addresses)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("FK_Customer_Address_CustomerId")
                .OnDelete(DeleteBehavior.Cascade);         
        }
    }
}