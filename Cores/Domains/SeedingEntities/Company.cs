using Domains.Entities;

namespace Domains.SeedingEntities
{
    public static partial class SeedingEntities
    {
        public static readonly Company DEFAULT_COMPANY = new Company()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000001"),
            Name = "LQC Company",
            Address = "",
            Phone = "1234567890",
        };
    }
}
