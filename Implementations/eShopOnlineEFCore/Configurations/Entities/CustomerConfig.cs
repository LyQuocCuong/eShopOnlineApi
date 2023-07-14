using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopOnlineEFCore.Configurations.Entities
{
    internal sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            ConfigBaseAttributes(builder);
            ConfigColumnOrder(builder);

            // Relationships
            // ---

        }

        private void ConfigBaseAttributes(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));
            builder.HasKey(t => new { t.Id });
            builder.Property(t => t.Id).IsRequired();
        }

        private void ConfigColumnOrder(EntityTypeBuilder<Customer> builder)
        {
            int index = 0;
            builder.Property(props => props.Id).HasColumnOrder(++index);
            builder.Property(props => props.Code).HasColumnOrder(++index);
            builder.Property(props => props.FirstName).HasColumnOrder(++index);
            builder.Property(props => props.LastName).HasColumnOrder(++index);
            builder.Property(props => props.Address).HasColumnOrder(++index);
            builder.Property(props => props.Phone).HasColumnOrder(++index);
            builder.Property(props => props.IsRemoved).HasColumnOrder(++index);
            builder.Property(props => props.CreatedDate).HasColumnOrder(++index);
            builder.Property(props => props.UpdatedDate).HasColumnOrder(++index);
        }

    }
}
