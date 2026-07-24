using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.State)
                .HasConversion<string>()
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Street)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Neighborhood)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Complement)
                .HasMaxLength(255);

            builder.Property(x => x.Number)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.ZipCode)
                .HasMaxLength(9)
                .IsRequired();
        }
    }
}