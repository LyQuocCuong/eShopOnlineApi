using Domains.Entities;

namespace Domains.SeedingEntities
{
    public static partial class SeedingEntities
    {
        public static readonly Employee ROOT_ADMIN = new Employee()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000101"),
            WorkingStoreId = null,
            Code = "ADMIN101",
            FirstName = "Henry",
            LastName = "Admin",
            Address = "",
            Phone = "",
        };
    }
}
