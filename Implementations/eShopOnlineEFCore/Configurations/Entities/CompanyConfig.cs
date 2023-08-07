namespace eShopOnlineEFCore.Configurations.Entities
{
    internal sealed class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            ConfigBaseAttributes(builder);
            ConfigColumnOrder(builder);
            ConfigSeedingData(builder);
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
            builder.Property(props => props.IsDeleted).HasColumnOrder(++index);
            builder.Property(props => props.CreatedDateUtcZero).HasColumnOrder(++index);
            builder.Property(props => props.UpdatedDateUtcZero).HasColumnOrder(++index);
        }

        private void ConfigSeedingData(EntityTypeBuilder<Company> builder)
        {
            List<Company> companies = new List<Company>()
            {
                new Company()
                {
                    Id = SeedingEntities.DEFAULT_COMPANY.Id,
                    Name = SeedingEntities.DEFAULT_COMPANY.Name,
                    Address = SeedingEntities.DEFAULT_COMPANY.Address,
                    Phone = SeedingEntities.DEFAULT_COMPANY.Phone,
                    IsDeleted = SeedingEntities.DefaultIsDeleted,
                    CreatedDateUtcZero = SeedingEntities.DefaultCreatedDateUtcZero,
                    UpdatedDateUtcZero = SeedingEntities.DefaultUpdatedDateUtcZero
                }
            };
            builder.HasData(companies);
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
