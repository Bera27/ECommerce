using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.Weight)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.Height)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.Width)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.Length)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.Quantity);

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasIndex(x => x.Name);

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_Image_Product_ProductId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}