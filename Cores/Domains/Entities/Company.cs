namespace Domains.Entities
{
    public sealed class Company : BaseEntity
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
