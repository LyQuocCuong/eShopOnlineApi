namespace UnitTest.eShopOnlineApiRestful.Controllers
{
    [TestFixture]
    [TestOf(typeof(EmployeeController))]
    public sealed class EmployeeControllerTest : AbstractControllerTest<EmployeeController>
    {
        private EmployeeController _mockEmployeeController;

        public override void SetUpBeforeExecutingEachTest()
        {
            _mockEmployeeController = new EmployeeController(_stubLogger.Object,
                                                             _stubControllerParams.Object);
        }

        #region GetAllEmployeesAsync

        private void Mock_Employee_GetAllAsync(IEnumerable<EmployeeDto> returnedEmployees)
        {
            _stubServices
                .Setup(s => s.Employee.GetAllAsync())
                .ReturnsAsync(returnedEmployees);
        }

        private async Task<IActionResult> Act_GetAllEmployeesAsync()
        {
            return await _mockEmployeeController.GetAllEmployeesAsync();
        }

        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Arrange
            Mock_Employee_GetAllAsync(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = await Act_GetAllEmployeesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_ValidDataType()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Arrange
            Mock_Employee_GetAllAsync(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = await Act_GetAllEmployeesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjResult = (OkObjectResult) actionResult;
            Assert.That(okObjResult.Value, Is.Not.Null);
            Assert.That(okObjResult.Value, Is.InstanceOf<IEnumerable<EmployeeDto>>());
        }

        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_SameAmountItems()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Data for Assertion
            var expectedAmountItems = listOfEmployees.Count();

            // Arrange
            Mock_Employee_GetAllAsync(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = await Act_GetAllEmployeesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjectResult = (OkObjectResult)actionResult;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<EmployeeDto>>());

            var actualEmployeesCollection = (IEnumerable<EmployeeDto>)okObjectResult.Value;
            Assert.That(
                actualEmployeesCollection.Count(), Is.EqualTo(expectedAmountItems),
                "GetAllEmployees() returns an insufficient number of IEnumerable<EmployeeDto> records."
            );
        }
        
        [Test]
        [Category("[Action] GetAllEmployeesAsync")]
        public async Task GetAllEmployeesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_UniqueCompanyIds()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Arrange
            Mock_Employee_GetAllAsync(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = await Act_GetAllEmployeesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjectResult = (OkObjectResult)actionResult;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<EmployeeDto>>());

            var actualEmployeesCollection = (IEnumerable<EmployeeDto>)okObjectResult.Value;
            var employeeIds = actualEmployeesCollection.Select(c => c.Id);
            Assert.That(
                employeeIds, Is.Unique,
                "Collection data returned from GetAllEmployees() is DUPLICATED in the EmployeeId field."
            );
        }

        #endregion

        #region GetEmployeeByIdAsync

        private void Mocking_Employee_GetByIdAsync(Guid employeeId, EmployeeDto? returnedEmployee)
        {
            _stubServices
                .Setup(s => s.Employee.GetByIdAsync(employeeId))
                .ReturnsAsync(returnedEmployee);
        }

        private async Task<IActionResult> Act_GetEmployeeByIdAsync(Guid employeeId)
        {
            return await _mockEmployeeController.GetEmployeeByIdAsync(employeeId);
        }

        [Test]
        [Category("[Action] GetEmployeeByIdAsync")]
        public async Task GetEmployeeByIdAsync_Inputs_ExistingEmployeeId_Returns_OkObjectResult()
        {
            // Inputs for Act
            var existingEmployee = _fakeDataManager.Employee.GenerateEmployeeNo1();

            // Arrange
            Mocking_Employee_GetByIdAsync(
                employeeId: existingEmployee.Id,
                returnedEmployee: existingEmployee
            );

            // Act
            var actionResult = await Act_GetEmployeeByIdAsync(employeeId: existingEmployee.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetEmployeeByIdAsync")]
        public async Task GetEmployeeByIdAsync_Inputs_ExistingEmployeeId_Returns_OkObjectResult_Include_Object_Has_ValidDataType()
        {
            // Inputs for Act
            var existingEmployee = _fakeDataManager.Employee.GenerateEmployeeNo1();

            // Arrange
            Mocking_Employee_GetByIdAsync(
                employeeId: existingEmployee.Id,
                returnedEmployee: existingEmployee
            );

            // Act
            var actionResult = await Act_GetEmployeeByIdAsync(employeeId: existingEmployee.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var actualData = ((OkObjectResult)actionResult).Value;
            Assert.That(actualData, Is.Not.Null);
            Assert.That(actualData, Is.InstanceOf<EmployeeDto>());
        }

        [Test]
        [Category("[Action] GetEmployeeByIdAsync")]
        public async Task GetEmployeeByIdAsync_Inputs_NonExistingEmployeeId_Returns_NotFoundObjectResult_Include_MessageString()
        {
            // Inputs for Act
            Guid nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();

            // Arrange
            Mocking_Employee_GetByIdAsync(
                employeeId: nonExistingEmployeeId,
                returnedEmployee: null
            );

            // Act
            var actionResult = await Act_GetEmployeeByIdAsync(employeeId: nonExistingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NotFoundObjectResult>());

            var message = ((NotFoundObjectResult)actionResult).Value;
            Assert.That(message, Is.Not.Null);
            Assert.That(message, Is.InstanceOf<string>());
        }

        #endregion

        #region CreateEmployeeAsync

        private void Mocking_Employee_CreateAsync(EmployeeForCreationDto creationDto,
                                              EmployeeDto returnedEmployeeDto)
        {
            _stubServices
                .Setup(s => s.Employee.CreateAsync(creationDto))
                .ReturnsAsync(returnedEmployeeDto);
        }

        private async Task<IActionResult> Act_CreateEmployeeAsync(EmployeeForCreationDto? creationDto)
        {
            return await _mockEmployeeController.CreateEmployeeAsync(creationDto);
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Input_NullCreateObj_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            EmployeeForCreationDto? creationDataObj = null;

            // Arrange

            // Act
            var actionResult = await Act_CreateEmployeeAsync(
                creationDto: creationDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Inputs_NotNullCreateObj_Returns_CreatedAtRouteResult()
        {
            // Inputs for Act
            var creationDataObj = _fakeDataManager.Employee.GenerateCreationDataObj();

            // Arrange
            var newlyCreatedEmployee = _fakeDataManager.Employee.GenerateEmployeeNo2();

            Mocking_Employee_CreateAsync(
                creationDto: creationDataObj,
                returnedEmployeeDto: newlyCreatedEmployee
            );

            // Act
            var actionResult = await Act_CreateEmployeeAsync(
                creationDto: creationDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(CreatedAtRouteResult)));
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Inputs_NotNullCreateObj_Returns_CreatedAtRouteResult_Include_Object_Has_ValidDataType()
        {
            // Inputs for Act
            var creationDataObj = _fakeDataManager.Employee.GenerateCreationDataObj();

            // Arrange
            var newlyCreatedEmployee = _fakeDataManager.Employee.GenerateEmployeeNo2();

            Mocking_Employee_CreateAsync(
                creationDto: creationDataObj,
                returnedEmployeeDto: newlyCreatedEmployee
            );

            // Act
            var actionResult = await Act_CreateEmployeeAsync(
                creationDto: creationDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(CreatedAtRouteResult)));

            var actualObj = ((CreatedAtRouteResult)actionResult).Value;
            Assert.That(actualObj, Is.Not.Null);
            Assert.That(actualObj, Is.InstanceOf<EmployeeDto>());
        }

        [Test]
        [Category("[Action] CreateEmployeeAsync")]
        public async Task CreateEmployeeAsync_Inputs_NotNullCreateObj_Returns_CreatedAtRouteResult_Include_Object_Is_ReturnedFromService()
        {
            // Inputs for Act
            var creationDataObj = _fakeDataManager.Employee.GenerateCreationDataObj();

            // Arrange
            var newlyCreatedEmployee = _fakeDataManager.Employee.GenerateEmployeeNo2();

            Mocking_Employee_CreateAsync(
                creationDto: creationDataObj,
                returnedEmployeeDto: newlyCreatedEmployee
            );

            // Act
            var actionResult = await Act_CreateEmployeeAsync(
                creationDto: creationDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(CreatedAtRouteResult)));

            var actualObj = ((CreatedAtRouteResult)actionResult).Value;
            Assert.That(actualObj, Is.Not.Null);
            Assert.That(actualObj, Is.InstanceOf<EmployeeDto>());

            var actualNewEmployee = (EmployeeDto)actualObj;
            Assert.That(actualNewEmployee, Is.EqualTo(newlyCreatedEmployee));
        }

        #endregion

        #region UpdateEmployeeFullyAsync

        private void Mocking_Employee_IsValidIdAsync(Guid employeeId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.IsValidIdAsync(employeeId))
                .ReturnsAsync(returnedResult);
        }

        private void Mocking_Employee_UpdateFullyAsync(Guid employeeId,
                                                 EmployeeForUpdateDto updateDto,
                                                 bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.UpdateFullyAsync(employeeId, updateDto))
                .ReturnsAsync(returnedResult);
        }

        private async Task<IActionResult> Act_UpdateEmployeeFullyAsync(Guid employeeId, EmployeeForUpdateDto? updateDto)
        {
            return await _mockEmployeeController.UpdateEmployeeFullyAsync(employeeId, updateDto);
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_NonExistingEmployeeId_And_OtherExpectedInputs_Returns_NotFoundObjResult()
        {
            // Inputs for Act
            Guid nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();
            var updateDataObj = _fakeDataManager.Employee.GenerateUpdateDataObj();

            // Arrange
            Mocking_Employee_IsValidIdAsync(
                employeeId: nonExistingEmployeeId,
                returnedResult: false
            );

            // Act
            var actionResult = await Act_UpdateEmployeeFullyAsync(
                employeeId: nonExistingEmployeeId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NotFoundObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            EmployeeForUpdateDto? updateDataObj = null;
            Guid existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;

            // Arrange
            Mocking_Employee_IsValidIdAsync(
                employeeId: existingEmployeeId,
                returnedResult: true
            );

            // Act
            var actionResult = await Act_UpdateEmployeeFullyAsync(
                employeeId: existingEmployeeId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFullyAsync")]
        public async Task UpdateEmployeeFullyAsync_Inputs_ExpectedData_Returns_NoContentResult()
        {
            // Inputs for Act
            Guid existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;
            var updateDataObj = _fakeDataManager.Employee.GenerateUpdateDataObj();

            // Arrange
            Mocking_Employee_IsValidIdAsync(
                employeeId: existingEmployeeId,
                returnedResult: true
            );
            Mocking_Employee_UpdateFullyAsync(
                employeeId: existingEmployeeId,
                updateDto: updateDataObj,
                returnedResult: true
            );

            // Act
            var actionResult = await Act_UpdateEmployeeFullyAsync(
                employeeId: existingEmployeeId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NoContentResult)));
        }

        #endregion

        #region DeleteEmployeeSoftlyAsync

        private void Mocking_Employee_DeleteSoftlyAsync(Guid employeeId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.DeleteSoftlyAsync(employeeId))
                .ReturnsAsync(returnedResult);
        }

        private async Task<IActionResult> Act_DeleteEmployeeSoftlyAsync(Guid employeeId)
        {
            return await _mockEmployeeController.DeleteEmployeeSoftlyAsync(employeeId);
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_NonExistingEmployeeId_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            var nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();

            // Arrange
            Mocking_Employee_DeleteSoftlyAsync(
                employeeId: nonExistingEmployeeId,
                returnedResult: false
            );

            // Act
            var actionResult = await Act_DeleteEmployeeSoftlyAsync(nonExistingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftlyAsync")]
        public async Task DeleteEmployeeSoftlyAsync_Inputs_ExistingEmployeeId_Returns_NoContentObjResult()
        {
            // Inputs for Act
            var existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;

            // Arrange
            Mocking_Employee_DeleteSoftlyAsync(
                employeeId: existingEmployeeId,
                returnedResult: true
            );

            // Act
            var actionResult = await Act_DeleteEmployeeSoftlyAsync(existingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NoContentResult>());
        }

        #endregion

        #region DeleteEmployeeHardAsync

        private void Mocking_Employee_DeleteHardAsync(Guid employeeId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.DeleteHardAsync(employeeId))
                .ReturnsAsync(returnedResult);
        }

        private async Task<IActionResult> Act_DeleteEmployeeHardAsync(Guid employeeId)
        {
            return await _mockEmployeeController.DeleteEmployeeHardAsync(employeeId);
        }

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_NonExistingEmployeeId_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            var nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();

            // Arrange
            Mocking_Employee_DeleteHardAsync(
                employeeId: nonExistingEmployeeId,
                returnedResult: false
            );

            // Act
            var actionResult = await Act_DeleteEmployeeHardAsync(nonExistingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        [Category("[Action] DeleteEmployeeHardAsync")]
        public async Task DeleteEmployeeHardAsync_Inputs_ExistingEmployeeId_Returns_NoContentObjResult()
        {
            // Inputs for Act
            var existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;

            // Arrange
            Mocking_Employee_DeleteHardAsync(
                employeeId: existingEmployeeId,
                returnedResult: true
            );

            // Act
            var actionResult = await Act_DeleteEmployeeHardAsync(existingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NoContentResult>());
        }

        #endregion

    }
}
