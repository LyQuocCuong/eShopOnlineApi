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
            builder.Property(props => props.IsDeleted).HasColumnOrder(++index);
            builder.Property(props => props.CreatedDateUtcZero).HasColumnOrder(++index);
            builder.Property(props => props.UpdatedDateUtcZero).HasColumnOrder(++index);
        }

        private void ConfigSeedingData(EntityTypeBuilder<Employee> builder)
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = SeedingEntities.ROOT_ADMIN.Id,
                    WorkingStoreId = SeedingEntities.ROOT_ADMIN.WorkingStoreId,
                    Code = SeedingEntities.ROOT_ADMIN.Code,
                    FirstName = SeedingEntities.ROOT_ADMIN.FirstName,
                    LastName = SeedingEntities.ROOT_ADMIN.LastName,
                    Phone = SeedingEntities.ROOT_ADMIN.Phone,
                    IsDeleted = SeedingEntities.DefaultIsDeleted,
                    CreatedDateUtcZero = SeedingEntities.DefaultCreatedDateUtcZero,
                    UpdatedDateUtcZero = SeedingEntities.DefaultUpdatedDateUtcZero
                }
            };
            builder.HasData(employees);
        }

    }
}
