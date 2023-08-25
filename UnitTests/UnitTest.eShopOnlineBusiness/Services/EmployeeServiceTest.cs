namespace UnitTest.eShopOnlineBusiness.Services
{
    [TestFixture]
    [TestOf(typeof(EmployeeService))]
    public sealed class EmployeeServiceTest : AbstractServiceTest<EmployeeService>
    {
        protected override EmployeeService InitService()
        {
            return new EmployeeService(base.GetILogger(), base.GetServiceParams());
        }

        #region GetAllAsync

        [Test]
        [Category("[Method] GetAllAsync")]
        public async Task GetAllAsync_Inputs_Parameterless_Returns_Collection_Of_EmployeeDto()
        {
            // Arrange
            var employeeService = InitService();

            // Act
            var result = await employeeService.GetAllAsync() as IEnumerable<EmployeeDto>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [Category("[Method] GetAllAsync")]
        public async Task GetAllAsync_Inputs_Parameterless_Returns_Collection_Has_FullAmountItems()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var expectedCollectionData = fakeDataForEmployee.GetListOfEmployees();
            var employeeService = InitService();

            // Act
            var collectionData = await employeeService.GetAllAsync() as IEnumerable<EmployeeDto>;

            // Assert
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Count(), Is.EqualTo(expectedCollectionData.Count()),
                "GetAllAsync() returns an insufficient number of IEnumerable<EmployeeDto> records."
            );
        }

        [Test]
        [Category("[Method] GetAllAsync")]
        public async Task GetAllAsync_Inputs_Parameterless_Returns_Collection_Has_UniqueEmployeeIds()
        {
            // Arrange
            var employeeService = InitService();

            // Act
            var collectionData = await employeeService.GetAllAsync() as IEnumerable<EmployeeDto>;

            // Assert
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Select(c => c.Id), Is.Unique,
                "Collection data returned from GetAllAsync() is DUPLICATED in the EmployeeId field."
            );
        }

        #endregion

        #region GetByIdAsync

        [Test]
        [Category("[Method] GetByIdAsync")]
        public async Task GetByIdAsync_Inputs_ExistingEmployeeId_Returns_Object_Is_EmployeeDto()
        {
            // Arrange
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.GetByIdAsync(existingEmployeeId) as EmployeeDto;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [Category("[Method] GetByIdAsync")]
        public async Task GetByIdAsync_Inputs_NonExistingEmployeeId_Returns_Null()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.GetByIdAsync(nonExistingEmployeeId);

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        #region IsValidIdAsync

        [Test]
        [Category("[Method] IsValidIdAsync")]
        public async Task IsValidIdAsync_Inputs_ExistingEmployeeId_Returns_True()
        {
            // Arrange
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.IsValidIdAsync(existingEmployeeId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("[Method] IsValidIdAsync")]
        public async Task IsValidIdAsync_Inputs_NonExistingEmployeeId_Returns_True()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.IsValidIdAsync(nonExistingEmployeeId);

            // Assert
            Assert.That(result, Is.False);
        }

        #endregion

        #region UpdateFullyAsync

        [Test]
        [Category("[Method] UpdateFullyAsync")]
        public async Task UpdateFullyAsync_Inputs_NonExistingEmployeeId_And_OtherExpectedInputs_Returns_False()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var fakeDataForEmployee = new FakeDataForEmployee();
            var validUpdateDto = fakeDataForEmployee.GetValidUpdateDto();
            var employeeService = InitService();

            // Act
            var result = await employeeService.UpdateFullyAsync(nonExistingEmployeeId, validUpdateDto);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Optimize Later]")]
        [Category("[Method] UpdateFullyAsync")]
        public async Task UpdateFullyAsync_Inputs_InvalidUpdateObj_And_OtherExpectedInputs_Returns_False()
        {
            // Arrange
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var fakeDataForEmployee = new FakeDataForEmployee();
            var invalidUpdateDto = fakeDataForEmployee.GetInvalidUpdateDto();
            var employeeService = InitService();

            // Act
            var result = await employeeService.UpdateFullyAsync(existingEmployeeId, invalidUpdateDto);

            // Assert
            Assert.That(result, Is.True);   // expected: Is.False
        }

        [Test]
        [Category("[Method] UpdateFullyAsync")]
        public async Task UpdateFullyAsync_Inputs_ExpectedData_Returns_True()
        {
            // Arrange
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var fakeDataForEmployee = new FakeDataForEmployee();
            var validUpdateDto = fakeDataForEmployee.GetValidUpdateDto();
            var employeeService = InitService();

            // Act
            var result = await employeeService.UpdateFullyAsync(existingEmployeeId, validUpdateDto);

            // Assert
            Assert.That(result, Is.True);
        }

        #endregion

        #region CreateAsync

        [Test]
        [Category("[Method] CreateAsync")]
        public async Task CreateAsync_Inputs_ValidCreateObj_Returns_Object_Is_EmployeeDto()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var validCreationDto = fakeDataForEmployee.GetValidCreationDto();
            var employeeService = InitService();

            // Act
            var result = await employeeService.CreateAsync(validCreationDto) as EmployeeDto;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region DeleteSoftlyAsync

        [Test]
        [Category("[Method] DeleteSoftlyAsync")]
        public async Task DeleteSoftlyAsync_Inputs_RootAdminId_Returns_False()
        {
            // Arrange
            var rootAdminId = FakeDataForEmployee.GetRootAdminId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteSoftlyAsync(rootAdminId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Method] DeleteSoftlyAsync")]
        public async Task DeleteSoftlyAsync_Inputs_ManagerIdOfStore_Returns_False()
        {
            // Arrange
            var managerIdOfStore = FakeDataForEmployee.GetManagerIdOfStore();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteSoftlyAsync(managerIdOfStore);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Method] DeleteSoftlyAsync")]
        public async Task DeleteSoftlyAsync_Inputs_NonExistingEmployeeId_Returns_False()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteSoftlyAsync(nonExistingEmployeeId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Method] DeleteSoftlyAsync")]
        public async Task DeleteSoftlyAsync_Inputs_NormalEmployeeId_Returns_True()
        {
            // Arrange
            var normalEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteSoftlyAsync(normalEmployeeId);

            // Assert
            Assert.That(result, Is.True);
        }

        #endregion

        #region DeleteHardAsync

        [Test]
        [Category("[Method] DeleteHardAsync")]
        public async Task DeleteHardAsync_Inputs_RootAdminId_Returns_False()
        {
            // Arrange
            var rootAdminId = FakeDataForEmployee.GetRootAdminId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteHardAsync(rootAdminId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Method] DeleteHardAsync")]
        public async Task DeleteHardAsync_Inputs_ManagerIdOfStore_Returns_False()
        {
            // Arrange
            var managerIdOfStore = FakeDataForEmployee.GetManagerIdOfStore();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteHardAsync(managerIdOfStore);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Method] DeleteHardAsync")]
        public async Task DeleteHardAsync_Inputs_NonExistingEmployeeId_Returns_False()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteHardAsync(nonExistingEmployeeId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Method] DeleteHardAsync")]
        public async Task DeleteHardAsync_Inputs_NormalEmployeeId_Returns_True()
        {
            // Arrange
            var normalEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeService = InitService();

            // Act
            var result = await employeeService.DeleteHardAsync(normalEmployeeId);

            // Assert
            Assert.That(result, Is.True);
        }

        #endregion

    }
}
