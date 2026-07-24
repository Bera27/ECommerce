using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Mappings
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Url)
                .HasMaxLength(2048)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasMaxLength(5)
                .IsRequired();
        }
    }
}