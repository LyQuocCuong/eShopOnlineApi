namespace Shared.DTOs.Inputs.FromBody.CreationDtos
{
    public sealed class CustomerForCreationDto
    {
        public string? Code { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
