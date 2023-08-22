namespace FakeDataShared.FakeDataForEntities
{
    public sealed class FakeDataForEmployee
    {
        public Guid GetUnableDeletedEmployeeId()
        {
            return new Guid("00000000-0000-0000-0000-000000000100");
        }

        public Guid GetExistingEmployeeId()
        {
            return new Guid("00000000-0000-0000-0000-000000000102");
        }

        public Guid GetNewlyCreatedEmployeeId()
        {
            return new Guid("00000000-0000-0000-0000-000000000105");
        }

        public Guid GetNonExistingEmployeeId()
        {
            return new Guid("00000000-0000-0000-0000-000000000109");
        }

        public IEnumerable<EmployeeDto> GetListOfEmployeeDtos()
        {
            return new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Id = this.GetExistingEmployeeId(),
                    Code = "E001"
                }
            };
        }

        public EmployeeForUpdateDto GetValidUpdateDto()
        {
            return new EmployeeForUpdateDto()
            {
                Code = "Valid Object",
            };
        }

        public EmployeeForUpdateDto GetInvalidUpdateDto()
        {
            return new EmployeeForUpdateDto()
            {
                Code = "Invalid Object",
            };
        }

        public EmployeeForCreationDto GetValidCreationDto()
        {
            return new EmployeeForCreationDto()
            {
                Code = "Valid Object",
            };
        }

        public EmployeeDto GetNewlyCreatedEmployeeDto()
        {
            return new EmployeeDto()
            {
                Id = this.GetNewlyCreatedEmployeeId(),
                Code = "NewlyCreated"
            };
        }

    }
}
