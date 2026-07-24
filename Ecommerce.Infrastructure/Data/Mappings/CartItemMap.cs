using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class CartItemMap : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("FK_CartItem_Product_ProductId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}