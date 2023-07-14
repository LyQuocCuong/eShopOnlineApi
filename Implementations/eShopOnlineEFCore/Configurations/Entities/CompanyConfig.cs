using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopOnlineEFCore.Configurations.Entities
{
    internal sealed class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            ConfigBaseAttributes(builder);
            ConfigColumnOrder(builder);
            // Relationships
            ConfigManyStoresRelationship(builder);  // 1 Company -> N Stores
            // ---
        }
                
        private void ConfigBaseAttributes(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(nameof(Company));
            builder.HasKey(t => new { t.Id });
            builder.Property(t => t.Id).IsRequired();
        }

        private void ConfigColumnOrder(EntityTypeBuilder<Company> builder)
        {
            int index = 0;
            builder.Property(props => props.Id).HasColumnOrder(++index);
            builder.Property(props => props.Name).HasColumnOrder(++index);
            builder.Property(props => props.Address).HasColumnOrder(++index);
            builder.Property(props => props.Phone).HasColumnOrder(++index);
            builder.Property(props => props.IsRemoved).HasColumnOrder(++index);
            builder.Property(props => props.CreatedDate).HasColumnOrder(++index);
            builder.Property(props => props.UpdatedDate).HasColumnOrder(++index);
        }

        private void ConfigManyStoresRelationship(EntityTypeBuilder<Company> builder)
        {
            builder.HasMany<Store>(c => c.Stores)
                .WithOne(s => s.Company)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
