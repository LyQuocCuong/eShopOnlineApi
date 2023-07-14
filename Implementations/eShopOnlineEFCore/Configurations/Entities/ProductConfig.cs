using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopOnlineEFCore.Configurations.Entities
{
    internal sealed class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ConfigBaseAttributes(builder);
            ConfigColumnOrder(builder);
            // Relationships
            // ---

        }

        private void ConfigBaseAttributes(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(t => new { t.Id });
            builder.Property(t => t.Id).IsRequired();
        }

        private void ConfigColumnOrder(EntityTypeBuilder<Product> builder)
        {
            int index = 0;
            builder.Property(props => props.Id).HasColumnOrder(++index);
            builder.Property(props => props.Code).HasColumnOrder(++index);
            builder.Property(props => props.Name).HasColumnOrder(++index);
            builder.Property(props => props.IsRemoved).HasColumnOrder(++index);
            builder.Property(props => props.CreatedDate).HasColumnOrder(++index);
            builder.Property(props => props.UpdatedDate).HasColumnOrder(++index);
        }

    }
}
