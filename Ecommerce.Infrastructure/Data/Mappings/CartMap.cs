using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class CartMap : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("FK_Cart_Customer_CustomerId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Cart)
                .HasForeignKey(x => x.CartId)
                .HasConstraintName("FK_Cart_CartItem_CartId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}