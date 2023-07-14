namespace Domains.Entities
{
    public sealed class Employee : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid? WorkingStoreId { get; set; }

        public string? Code { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public Store? ManagingStore { get; set; }

        public Store? WorkingStore { get; set; }
    }
}
