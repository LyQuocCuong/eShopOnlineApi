using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopOnlineEFCore.Configurations.Entities
{
    internal sealed class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            ConfigBaseAttributes(builder);
            ConfigColumnOrder(builder);
            // Relationships
            ConfigManyEmployeesRelationship(builder);   // 1 Store -> N Employees
            ConfigOneManagerRelationship(builder);      // 1 Store -> 1 Manager
            // ---
        }

        private void ConfigBaseAttributes(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable(nameof(Store));
            builder.HasKey(s => new { s.Id });
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.CompanyId).IsRequired();
        }

        private void ConfigColumnOrder(EntityTypeBuilder<Store> builder)
        {
            int index = 0;
            builder.Property(props => props.Id).HasColumnOrder(++index);
            builder.Property(props => props.CompanyId).HasColumnOrder(++index);
            builder.Property(props => props.ManagerId).HasColumnOrder(++index);
            builder.Property(props => props.Code).HasColumnOrder(++index);
            builder.Property(props => props.Name).HasColumnOrder(++index);
            builder.Property(props => props.Address).HasColumnOrder(++index);
            builder.Property(props => props.Phone).HasColumnOrder(++index);
            builder.Property(props => props.IsRemoved).HasColumnOrder(++index);
            builder.Property(props => props.CreatedDate).HasColumnOrder(++index);
            builder.Property(props => props.UpdatedDate).HasColumnOrder(++index);
        }

        private void ConfigManyEmployeesRelationship(EntityTypeBuilder<Store> builder)
        {
            builder.HasMany<Employee>(s => s.EmployeesList)
                .WithOne(e => e.WorkingStore)
                .HasForeignKey(s => s.WorkingStoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigOneManagerRelationship(EntityTypeBuilder<Store> builder)
        {
            builder.HasOne<Employee>(s => s.Manager)
                .WithOne(e => e.ManagingStore)
                .HasForeignKey<Store>(s => s.ManagerId);
        }

    }
}
