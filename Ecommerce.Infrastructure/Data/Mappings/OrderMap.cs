using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.OrderDate)
                .IsRequired();

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

            builder.Property(x => x.ShippingCost)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(x => x.Total)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .HasConstraintName("FK_Order_OrderItem_OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("FK_Order_Customer_CustomerId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}