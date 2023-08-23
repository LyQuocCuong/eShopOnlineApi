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
            var fakeDataForEmployee = new FakeDataForEmployee();
            var existingEmployeeId = fakeDataForEmployee.GetExistingEmployeeId();
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
            var fakeDataForEmployee = new FakeDataForEmployee();
            var existingEmployeeId = fakeDataForEmployee.GetExistingEmployeeId();
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
            var fakeDataForEmployee = new FakeDataForEmployee();
            var nonExistingEmployeeId = fakeDataForEmployee.GetNonExistingEmployeeId();
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
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var nonExistingEmployeeId = fakeDataForEmployee.GetNonExistingEmployeeId();
            var validUpdateDto = fakeDataForEmployee.GetValidUpdateDto();
            var employeeController = InitController();

            // Act
            var notFoundObjResult = await employeeController.UpdateEmployeeFullyAsync(nonExistingEmployeeId, validUpdateDto) as NotFoundObjectResult;

            // Assert
            Assert.That(notFoundObjResult, Is.Not.Null);
            Assert.That(notFoundObjResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var existingEmployeeId = fakeDataForEmployee.GetExistingEmployeeId();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.UpdateEmployeeFullyAsync(existingEmployeeId, null) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_InvalidUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var existingEmployeeId = fakeDataForEmployee.GetExistingEmployeeId();
            var invalidUpdateDto = fakeDataForEmployee.GetInvalidUpdateDto();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.UpdateEmployeeFullyAsync(existingEmployeeId, invalidUpdateDto) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_ExpectedData_Returns_NoContentResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var existingEmployeeId = fakeDataForEmployee.GetExistingEmployeeId();
            var validUpdateDto = fakeDataForEmployee.GetValidUpdateDto();
            var employeeController = InitController();

            // Act
            var noContentResult = await employeeController.UpdateEmployeeFullyAsync(existingEmployeeId, validUpdateDto) as NoContentResult;

            // Assert
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }

        #endregion

        #region CreateEmployeeAsync

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Input_NullCreateObj_Returns_BadRequestObjResult()
        {
            // Arrange
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.CreateEmployeeAsync(null) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Inputs_ValidCreateObj_Returns_CreatedAtRouteResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var validCreationDto = fakeDataForEmployee.GetValidCreationDto();
            var employeeController = InitController();

            // Act
            var routeResult = await employeeController.CreateEmployeeAsync(validCreationDto) as CreatedAtRouteResult;

            // Assert
            Assert.That(routeResult, Is.Not.Null);
            Assert.That(routeResult.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
            Assert.That(routeResult.RouteName, Is.EqualTo("GetEmployeeById"));
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Inputs_ValidCreateObj_Returns_CreatedAtRouteResult_Include_Object_Is_EmployeeDto()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var validCreationDto = fakeDataForEmployee.GetValidCreationDto();
            var employeeController = InitController();

            // Act
            var routeResult = await employeeController.CreateEmployeeAsync(validCreationDto) as CreatedAtRouteResult;

            // Assert
            Assert.That(routeResult, Is.Not.Null);
            Assert.That(routeResult.Value, Is.InstanceOf<EmployeeDto>());
        }


        #endregion

        #region DeleteEmployeeSoftlyAsync

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_UnableDeletedEmployeeId_Returns_BadRequestObjResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var unableDeletedEmployeeId = fakeDataForEmployee.GetUnableDeletedEmployeeId();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeSoftlyAsync(unableDeletedEmployeeId) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_AbleDeletedEmployeeId_Returns_NoContentObjResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var ableDeletedEmployeeId = fakeDataForEmployee.GetExistingEmployeeId();
            var employeeController = InitController();

            // Act
            var noContentResult = await employeeController.DeleteEmployeeSoftlyAsync(ableDeletedEmployeeId) as NoContentResult;

            // Assert
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }

        #endregion

        #region DeleteEmployeeHardAsync

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_UnableDeletedEmployeeId_Returns_BadRequestObjResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var unableDeletedEmployeeId = fakeDataForEmployee.GetUnableDeletedEmployeeId();
            var employeeController = InitController();

            // Act
            var badRequestObjResult = await employeeController.DeleteEmployeeHardAsync(unableDeletedEmployeeId) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_AbleDeletedEmployeeId_Returns_NoContentObjResult()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForEmployee();
            var ableDeletedEmployeeId = fakeDataForEmployee.GetExistingEmployeeId();
            var employeeController = InitController();

            // Act
            var noContentResult = await employeeController.DeleteEmployeeHardAsync(ableDeletedEmployeeId) as NoContentResult;

            // Assert
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }

        #endregion

    }
}
