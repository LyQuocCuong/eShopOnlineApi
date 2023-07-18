namespace Shared.DTOs.Inputs.FromBody.UpdateDtos
{
    public sealed class StoreForUpdateDto
    {
        public Guid? ManagerId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
