namespace UnitTest.eShopOnlineApiRestful.ControllerTests
{
    [TestFixture]
    [TestOf(typeof(EmployeeController))]
    public sealed class EmployeeControllerTest : AbstractControllerTest<EmployeeController>
    {
        protected override EmployeeController InitController()
        {
            return new EmployeeController(base.GetILogger(), base.GetControllerParams());
        }

        #region GetAllEmployeesAsync

        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult()
        {
            // Arrange
            var employeeController = InitController();

            // Act
            var okObjResult = await employeeController.GetAllEmployeesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Of_EmployeeDto()
        {
            // Arrange
            var employeeController = InitController();

            // Act
            var okObjResult = await employeeController.GetAllEmployeesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.Value, Is.InstanceOf<IEnumerable<EmployeeDto>>());
        }

        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_FullAmountItems()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var expectedCollectionData = fakeDataForEmployee.GetListOfEmployeeDtos();
            var employeeController = InitController();

            // Act
            var okObjResult = await employeeController.GetAllEmployeesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            var collectionData = okObjResult.Value as IEnumerable<EmployeeDto>;
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Count(), Is.EqualTo(expectedCollectionData.Count()),
                "GetAllEmployeesAsync() returns an insufficient number of IEnumerable<EmployeeDto> records."
            );
        }

        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_UniqueEmployeeIds()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var expectedCollectionData = fakeDataForEmployee.GetListOfEmployeeDtos();
            var employeeController = InitController();

            // Act
            var okObjResult = await employeeController.GetAllEmployeesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            var collectionData = okObjResult.Value as IEnumerable<EmployeeDto>;
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Select(c => c.Id), Is.Unique,
                "Collection data returned from GetAllEmployeesAsync() is DUPLICATED in the EmployeeId field."
            );
        }

        #endregion

        #region GetCompanyByIdAsync

        [Test]
        [Category("[Action] GetEmployeeByIdAsync")]
        public async Task GetEmployeeByIdAsync_Inputs_ExistingEmployeeId_Returns_OkObjectResult()
        {
            // Arrange
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeController = InitController();

            // Act
            var okObjResult = await employeeController.GetEmployeeByIdAsync(existingEmployeeId) as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        [Category("[Action] GetEmployeeByIdAsync")]
        public async Task GetEmployeeByIdAsync_Inputs_ExistingEmployeeId_Returns_OkObjectResult_Include_Object_Is_EmployeeDto()
        {
            // Arrange
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeController = InitController();

            // Act
            var okObjResult = await employeeController.GetEmployeeByIdAsync(existingEmployeeId) as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.Value, Is.InstanceOf<EmployeeDto>());
        }

        [Test]
        [Category("[Action] GetEmployeeByIdAsync")]
        public async Task GetEmployeeByIdAsync_Inputs_NonExistingEmployeeId_Returns_NotFoundObjectResult()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var employeeController = InitController();

            // Act
            var notFoundObjResult = await employeeController.GetEmployeeByIdAsync(nonExistingEmployeeId) as NotFoundObjectResult;

            // Assert
            Assert.That(notFoundObjResult, Is.Not.Null);
            Assert.That(notFoundObjResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        #endregion

        #region UpdateCompanyFullyAsync

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_NonExistingEmployeeId_And_OtherExpectedInputs_Returns_NotFoundObjResult()
        {
            // NonExistingEmployeeId
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            // OtherExpectedInputs
            var fakeDataForEmployee = new FakeDataForEmployee();
            var validUpdateDto = fakeDataForEmployee.GetValidUpdateDto();

            // Arrange
            var employeeController = InitController();
            var updateDtoValidator = new EmployeeForUpdateDtoValidator();

            // Act
            var notFoundObjResult = await employeeController.UpdateEmployeeFullyAsync(nonExistingEmployeeId, validUpdateDto, updateDtoValidator) as NotFoundObjectResult;

            // Assert
            Assert.That(notFoundObjResult, Is.Not.Null);
            Assert.That(notFoundObjResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // OtherExpectedInputs
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();

            // Arrange
            var employeeController = InitController();
            var updateDtoValidator = new EmployeeForUpdateDtoValidator();

            // Act
            var badRequestObjResult = await employeeController.UpdateEmployeeFullyAsync(existingEmployeeId, null, updateDtoValidator) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        private static IEnumerable<EmployeeForUpdateDto> GetInvalidUpdateDtoSource()
        {
            yield return new FakeDataForEmployee().GetInvalidUpdateDto();
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        [TestCaseSource(nameof(GetInvalidUpdateDtoSource))]
        public async Task UpdateEmployeeFullyAsync_Inputs_InvalidUpdateObj_And_OtherExpectedInputs_Returns_UnprocessableEntityObjResult(EmployeeForUpdateDto invalidUpdateDto)
        {
            // OtherExpectedInputs
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();

            // Arrange
            var employeeController = InitController();
            var updateDtoValidator = new EmployeeForUpdateDtoValidator();

            // Act
            var unprocessableEntityObjResult = await employeeController.UpdateEmployeeFullyAsync(existingEmployeeId, invalidUpdateDto, updateDtoValidator) as UnprocessableEntityObjectResult;

            // Assert
            Assert.That(unprocessableEntityObjResult, Is.Not.Null);
            Assert.That(unprocessableEntityObjResult.StatusCode, Is.EqualTo(StatusCodes.Status422UnprocessableEntity));
        }

        private static IEnumerable<EmployeeForUpdateDto> GetValidUpdateDtoSource()
        {
            yield return new FakeDataForEmployee().GetValidUpdateDto();
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        [TestCaseSource(nameof(GetValidUpdateDtoSource))]
        public async Task UpdateEmployeeFullyAsync_Inputs_ExpectedData_Returns_NoContentResult(EmployeeForUpdateDto validUpdateDto)
        {
            // ExpectedData
            var existingEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();

            // Arrange
            var employeeController = InitController();
            var updateDtoValidator = new EmployeeForUpdateDtoValidator();

            // Act
            var noContentResult = await employeeController.UpdateEmployeeFullyAsync(existingEmployeeId, validUpdateDto, updateDtoValidator) as NoContentResult;

            // Assert
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }

        #endregion

        #region CreateEmployeeAsync

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Input_NullCreationObj_Returns_BadRequestObjResult()
        {
            // Arrange
            var employeeController = InitController();
            var creationDtoValidator = new EmployeeForCreationDtoValidator();

            // Act
            var badRequestObjResult = await employeeController.CreateEmployeeAsync(null, creationDtoValidator) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        private static IEnumerable<EmployeeForCreationDto> GetInvalidCreationDtoSource()
        {
            yield return new FakeDataForEmployee().GetInvalidCreationDto();
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        [TestCaseSource(nameof(GetInvalidCreationDtoSource))]
        public async Task CreateEmployeeAsync_Inputs_InvalidCreationObj_Returns_UnprocessableEntityObjResult(EmployeeForCreationDto invalidCreationDto)
        {
            // Arrange
            var employeeController = InitController();
            var creationDtoValidator = new EmployeeForCreationDtoValidator();

            // Act
            var unprocessableEntityObjResult = await employeeController.CreateEmployeeAsync(invalidCreationDto, creationDtoValidator) as UnprocessableEntityObjectResult;

            // Assert
            Assert.That(unprocessableEntityObjResult, Is.Not.Null);
            Assert.That(unprocessableEntityObjResult.StatusCode, Is.EqualTo(StatusCodes.Status422UnprocessableEntity));
        }

        private static IEnumerable<EmployeeForCreationDto> GetValidCreationDtoSource()
        {
            yield return new FakeDataForEmployee().GetValidCreationDto();
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        [TestCaseSource(nameof(GetValidCreationDtoSource))]
        public async Task CreateEmployeeAsync_Inputs_ValidCreateObj_Returns_CreatedAtRouteResult(EmployeeForCreationDto validCreationDto)
        {
            // Arrange
            var employeeController = InitController();
            var creationDtoValidator = new EmployeeForCreationDtoValidator();

            // Act
            var routeResult = await employeeController.CreateEmployeeAsync(validCreationDto, creationDtoValidator) as CreatedAtRouteResult;

            // Assert
            Assert.That(routeResult, Is.Not.Null);
            Assert.That(routeResult.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
            Assert.That(routeResult.RouteName, Is.EqualTo(nameof(employeeController.GetEmployeeByIdAsync)));
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Inputs_ValidCreateObj_Returns_CreatedAtRouteResult_Include_Object_Is_EmployeeDto()
        {
            // ValidCreateObj
            var fakeDataForEmployee = new FakeDataForEmployee();
            var validCreationDto = fakeDataForEmployee.GetValidCreationDto();

            // Arrange
            var employeeController = InitController();
            var creationDtoValidator = new EmployeeForCreationDtoValidator();

            // Act
            var routeResult = await employeeController.CreateEmployeeAsync(validCreationDto, creationDtoValidator) as CreatedAtRouteResult;

            // Assert
            Assert.That(routeResult, Is.Not.Null);
            Assert.That(routeResult.Value, Is.InstanceOf<EmployeeDto>());
        }

        #endregion

        #region DeleteEmployeeSoftlyAsync

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_RootAdminId_Returns_BadRequestObjResult()
        {
            // Arrange
            var rootAdminId = FakeDataForEmployee.GetRootAdminId();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeSoftlyAsync(rootAdminId) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_ManagerIdOfStore_Returns_BadRequestObjResult()
        {
            // Arrange
            var managerIdOfStore = FakeDataForEmployee.GetManagerIdOfStore();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeSoftlyAsync(managerIdOfStore) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_NonExistingEmployeeId_Returns_BadRequestObjResult()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeSoftlyAsync(nonExistingEmployeeId) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_NormalEmployeeId_Returns_NoContentResult()
        {
            // Arrange
            var normalEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeController = InitController();

            // Act
            var noContentResult = await employeeController.DeleteEmployeeSoftlyAsync(normalEmployeeId) as NoContentResult;

            // Assert
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }

        #endregion

        #region DeleteEmployeeHardAsync

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_RootAdminId_Returns_BadRequestObjResult()
        {
            // Arrange
            var rootAdminId = FakeDataForEmployee.GetRootAdminId();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeHardAsync(rootAdminId) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_ManagerIdOfStore_Returns_BadRequestObjResult()
        {
            // Arrange
            var managerIdOfStore = FakeDataForEmployee.GetManagerIdOfStore();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeHardAsync(managerIdOfStore) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_NonExistingEmployeeId_Returns_BadRequestObjResult()
        {
            // Arrange
            var nonExistingEmployeeId = FakeDataForEmployee.GetNonExistingEmployeeId();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeHardAsync(nonExistingEmployeeId) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_NormalEmployeeId_Returns_NoContentResult()
        {
            // Arrange
            var normalEmployeeId = FakeDataForEmployee.GetNormalEmployeeId();
            var employeeController = InitController();

            // Act
            var noContentResult = await employeeController.DeleteEmployeeHardAsync(normalEmployeeId) as NoContentResult;

            // Assert
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }

        #endregion

    }
}
