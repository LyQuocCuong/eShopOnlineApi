namespace FakeDataShared.FakeDataForEntities
{
    public sealed class FakeDataForStore
    {
        public static Guid GetNonExistingStoreId()
        {
            return new Guid("00000000-0000-0000-0000-000000000001");
        }

        public static Guid GetNormalStoreId()
        {
            return new Guid("30000000-0000-0000-0000-000000000001");
        }

        public StoreDto GetNormalStoreDto()
        {
            return new StoreDto()
            {
                Id = GetNormalStoreId(),
                CompanyId = FakeDataForCompany.GetNormalCompanyId(),
                Code = "S001",
                Name = "Normal Store",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public Store GetNormalStore()
        {
            return new Store()
            {
                Id = GetNormalStoreId(),
                CompanyId = FakeDataForCompany.GetNormalCompanyId(),
                Code = "S001",
                Name = "Normal Store",
                Address = "xxx",
                Phone = "xxx"
            };
        }

    }
}
