namespace FakeDataShared.FakeDataForEntities
{
    public sealed class FakeDataForEmployee
    {
        public static Guid GetNonExistingEmployeeId()
        {
            return new Guid("00000000-0000-0000-0000-000000000001");
        }

        public static Guid GetNormalEmployeeId()
        {
            return new Guid("20000000-0000-0000-0000-000000000001");
        }

        public static Guid GetRootAdminId()
        {
            return new Guid(SeedingEntities.ROOT_ADMIN.Id.ToString());
        }

        public static Guid GetManagerIdOfStore()
        {
            return new Guid("20000000-0000-0000-0000-000000000002");
        }

        public static Guid GetNewlyCreatedEmployeeId()
        {
            return new Guid("20000000-0000-0000-0000-000000000011");
        }

        public EmployeeDto GetNormalEmployeeDto()
        {
            return new EmployeeDto()
            {
                Id = GetNormalEmployeeId(),
                WorkingStoreId = null,
                ManagingStore = null,
                Code = "E003",
                FirstName = "Normal",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public EmployeeDto GetRootAdminDto()
        {
            return new EmployeeDto()
            {
                Id = GetRootAdminId(),
                WorkingStoreId = null,
                ManagingStore = null,
                Code = "E001",
                FirstName = "Root Admin",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public EmployeeDto GetManagerDtoOfStore()
        {
            return new EmployeeDto()
            {
                Id = GetManagerIdOfStore(),
                WorkingStoreId = null,
                ManagingStore = new FakeDataForStore().GetNormalStoreDto(),
                Code = "E002",
                FirstName = "Manager",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public IEnumerable<EmployeeDto> GetListOfEmployeeDtos()
        {
            return new List<EmployeeDto>()
            {
                GetNormalEmployeeDto(),
                GetRootAdminDto(),
                GetManagerDtoOfStore(),
            };
        }

        public EmployeeDto GetNewlyCreatedEmployeeDto()
        {
            return new EmployeeDto()
            {
                Id = GetNewlyCreatedEmployeeId(),
                WorkingStoreId = null,
                Code = "E00x",
                FirstName = "New",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public EmployeeForUpdateDto GetValidUpdateDto()
        {
            return new EmployeeForUpdateDto()
            {
                Code = "Valid",     // used for Mocking comparision (Valid / Invalid)
                FirstName = "Valid",
                LastName = "Object",
                Address = "yyy",
                Phone = "yyy"
            };
        }

        public EmployeeForUpdateDto GetInvalidUpdateDto()
        {
            return new EmployeeForUpdateDto()
            {
                Code = "Invalid",   // used for Mocking comparision (Valid / Invalid)
                FirstName = "Invalid",
                LastName = "Object",
                Address = "yyy",
                Phone = "yyy"
            };
        }

        public EmployeeForCreationDto GetValidCreationDto()
        {
            return new EmployeeForCreationDto()
            {
                Code = "Valid",     // used for Mocking comparision (Valid / Invalid)
                FirstName = "Valid",
                LastName = "Object",
                Address = "yyy",
                Phone = "yyy"
            };
        }

        public Employee GetNormalEmployee()
        {
            return new Employee()
            {
                Id = GetNormalEmployeeId(),
                WorkingStoreId = null,
                Code = "E001",
                FirstName = "Normal",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public Employee GetRootAdmin()
        {
            return new Employee()
            {
                Id = GetRootAdminId(),
                WorkingStoreId = null,
                Code = "E001",
                FirstName = "Root Admin",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public Employee GetManagerOfStore()
        {
            return new Employee()
            {
                Id = GetManagerIdOfStore(),
                WorkingStoreId = null,
                ManagingStore = new FakeDataForStore().GetNormalStore(),
                Code = "E001",
                FirstName = "Root Admin",
                LastName = "Employee",
                Address = "xxx",
                Phone = "xxx"
            };
        }

        public IEnumerable<Employee> GetListOfEmployees()
        {
            return new List<Employee>()
            {
                GetNormalEmployee(),
                GetRootAdmin(),
                GetManagerOfStore(),
            };
        }

    }
}
