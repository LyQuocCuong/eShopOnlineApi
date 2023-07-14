using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopOnlineEFCore.Configurations.Entities
{
    internal sealed class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            ConfigBaseAttributes(builder);
            ConfigColumnOrder(builder);
            ConfigSeedingData(builder);
            // Relationships
            // ---

        }

        private void ConfigBaseAttributes(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee));
            builder.HasKey(t => new { t.Id });
            builder.Property(t => t.Id).IsRequired();
        }

        private void ConfigColumnOrder(EntityTypeBuilder<Employee> builder)
        {
            int index = 0;
            builder.Property(props => props.Id).HasColumnOrder(++index);
            builder.Property(props => props.WorkingStoreId).HasColumnOrder(++index);
            builder.Property(props => props.Code).HasColumnOrder(++index);
            builder.Property(props => props.FirstName).HasColumnOrder(++index);
            builder.Property(props => props.LastName).HasColumnOrder(++index);
            builder.Property(props => props.Address).HasColumnOrder(++index);
            builder.Property(props => props.Phone).HasColumnOrder(++index);
            builder.Property(props => props.IsRemoved).HasColumnOrder(++index);
            builder.Property(props => props.CreatedDate).HasColumnOrder(++index);
            builder.Property(props => props.UpdatedDate).HasColumnOrder(++index);
        }

        private void ConfigSeedingData(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(SeedingEntities.ROOT_ADMIN);
        }

    }
}
