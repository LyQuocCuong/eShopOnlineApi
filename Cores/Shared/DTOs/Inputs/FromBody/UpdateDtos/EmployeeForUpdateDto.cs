﻿namespace Shared.DTOs.Inputs.FromBody.UpdateDtos
{
    public sealed class EmployeeForUpdateDto
    {
        public string? Code { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
