namespace eShopOnlineTestingUtilities.FakeDataGenerators
{
    public sealed class EmployeeGenerator
    {
        public Guid GenerateNonExistingEmployeeId()
        {
            return new Guid("00000000-0000-0000-0000-000000000103");
        }

        public EmployeeDto GenerateEmployeeNo1()
        {
            return new EmployeeDto()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000101"),
                Code = "C101",
                FirstName = "01",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public EmployeeDto GenerateEmployeeNo2()
        {
            return new EmployeeDto()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000102"),
                Code = "C102",
                FirstName = "02",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public IEnumerable<EmployeeDto> GenerateAListOfEmployees()
        {
            return new List<EmployeeDto>()
            {
                GenerateEmployeeNo1(),
                GenerateEmployeeNo2(),
            };
        }

        public EmployeeForUpdateDto GenerateUpdateDataObj()
        {
            return new EmployeeForUpdateDto()
            {
                Code = "Edited C1001",
                FirstName = "Edited 01",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public EmployeeForCreationDto GenerateCreationDataObj()
        {
            return  new EmployeeForCreationDto()
            {
                Code = "new C1000",
                FirstName = "New",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

    }
}
