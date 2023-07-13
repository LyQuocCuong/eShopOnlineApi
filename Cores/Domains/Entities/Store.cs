namespace Domains.Entities
{
    public sealed class Store : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid? ManagerId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public Company Company { get; set; }

        public Employee? Manager { get; set; }
    }
}
