namespace Shared.DTOs.Outputs.EntityDtos
{
    public sealed class StoreDto : AbstractEntityDto
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid? ManagerId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
