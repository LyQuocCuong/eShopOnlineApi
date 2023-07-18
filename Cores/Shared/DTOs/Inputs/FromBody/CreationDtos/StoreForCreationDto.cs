namespace Shared.DTOs.Inputs.FromBody.CreationDtos
{
    public sealed class StoreForCreationDto
    {
        public Guid CompanyId { get; set; }

        public Guid? ManagerId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
