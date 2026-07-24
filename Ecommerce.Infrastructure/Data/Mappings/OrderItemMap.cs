using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.NameProduct)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Discount)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_OrderItem_Product_ProductId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}