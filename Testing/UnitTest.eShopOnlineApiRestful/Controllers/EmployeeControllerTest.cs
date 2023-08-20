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

        #region GetAllEmployees

        private void Mock_Employee_GetAll(IEnumerable<EmployeeDto> returnedEmployees)
        {
            _stubServices
                .Setup(s => s.Employee.GetAll())
                .Returns(returnedEmployees);
        }

        private IActionResult Act_GetAllEmployees()
        {
            return _mockEmployeeController.GetAllEmployees();
        }

        [Test]
        [Category("[Action] GetAllEmployees")]
        public void GetAllEmployees_Inputs_Parameterless_Returns_OkObjectResult()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Arrange
            Mock_Employee_GetAll(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = Act_GetAllEmployees();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetAllEmployees")]
        public void GetAllEmployees_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_ValidDataType()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Arrange
            Mock_Employee_GetAll(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = Act_GetAllEmployees();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjResult = (OkObjectResult) actionResult;
            Assert.That(okObjResult.Value, Is.Not.Null);
            Assert.That(okObjResult.Value, Is.InstanceOf<IEnumerable<EmployeeDto>>());
        }

        [Test]
        [Category("[Action] GetAllEmployees")]
        public void GetAllEmployees_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_SameAmountItems()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Data for Assertion
            var expectedAmountItems = listOfEmployees.Count();

            // Arrange
            Mock_Employee_GetAll(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = Act_GetAllEmployees();

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
        [Category("[Action] GetAllEmployees")]
        public void GetAllEmployees_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_UniqueCompanyIds()
        {
            // Inputs for Act
            var listOfEmployees = _fakeDataManager.Employee.GenerateAListOfEmployees();

            // Arrange
            Mock_Employee_GetAll(returnedEmployees: listOfEmployees);

            // Act
            var actionResult = Act_GetAllEmployees();

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

        #region GetEmployeeById

        private void Mocking_Employee_GetById(Guid employeeId, EmployeeDto? returnedEmployee)
        {
            _stubServices
                .Setup(s => s.Employee.GetById(employeeId))
                .Returns(returnedEmployee);
        }

        private IActionResult Act_GetEmployeeById(Guid employeeId)
        {
            return _mockEmployeeController.GetEmployeeById(employeeId);
        }

        [Test]
        [Category("[Action] GetEmployeeById")]
        public void GetEmployeeById_Inputs_ExistingEmployeeId_Returns_OkObjectResult()
        {
            // Inputs for Act
            var existingEmployee = _fakeDataManager.Employee.GenerateEmployeeNo1();

            // Arrange
            Mocking_Employee_GetById(
                employeeId: existingEmployee.Id,
                returnedEmployee: existingEmployee
            );

            // Act
            var actionResult = Act_GetEmployeeById(employeeId: existingEmployee.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetEmployeeById")]
        public void GetEmployeeById_Inputs_ExistingEmployeeId_Returns_OkObjectResult_Include_Object_Has_ValidDataType()
        {
            // Inputs for Act
            var existingEmployee = _fakeDataManager.Employee.GenerateEmployeeNo1();

            // Arrange
            Mocking_Employee_GetById(
                employeeId: existingEmployee.Id,
                returnedEmployee: existingEmployee
            );

            // Act
            var actionResult = Act_GetEmployeeById(employeeId: existingEmployee.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var actualData = ((OkObjectResult)actionResult).Value;
            Assert.That(actualData, Is.Not.Null);
            Assert.That(actualData, Is.InstanceOf<EmployeeDto>());
        }

        [Test]
        [Category("[Action] GetEmployeeById")]
        public void GetEmployeeById_Inputs_NonExistingEmployeeId_Returns_NotFoundObjectResult_Include_MessageString()
        {
            // Inputs for Act
            Guid nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();

            // Arrange
            Mocking_Employee_GetById(
                employeeId: nonExistingEmployeeId,
                returnedEmployee: null
            );

            // Act
            var actionResult = Act_GetEmployeeById(employeeId: nonExistingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NotFoundObjectResult>());

            var message = ((NotFoundObjectResult)actionResult).Value;
            Assert.That(message, Is.Not.Null);
            Assert.That(message, Is.InstanceOf<string>());
        }

        #endregion

        #region CreateEmployee

        private void Mocking_Employee_Create(EmployeeForCreationDto creationDto,
                                              EmployeeDto returnedEmployeeDto)
        {
            _stubServices
                .Setup(s => s.Employee.Create(creationDto))
                .Returns(returnedEmployeeDto);
        }

        private IActionResult Act_CreateEmployee(EmployeeForCreationDto? creationDto)
        {
            return _mockEmployeeController.CreateEmployee(creationDto);
        }

        [Test]
        [Category("[Action] CreateEmployee")]
        public void CreateEmployee_Input_NullCreateObj_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            EmployeeForCreationDto? creationDataObj = null;

            // Arrange

            // Act
            var actionResult = Act_CreateEmployee(
                creationDto: creationDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }

        [Test]
        [Category("[Action] CreateEmployee")]
        public void CreateEmployee_Inputs_NotNullCreateObj_Returns_CreatedAtRouteResult()
        {
            // Inputs for Act
            var creationDataObj = _fakeDataManager.Employee.GenerateCreationDataObj();

            // Arrange
            var newlyCreatedEmployee = _fakeDataManager.Employee.GenerateEmployeeNo2();

            Mocking_Employee_Create(
                creationDto: creationDataObj,
                returnedEmployeeDto: newlyCreatedEmployee
            );

            // Act
            var actionResult = Act_CreateEmployee(
                creationDto: creationDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(CreatedAtRouteResult)));
        }

        [Test]
        [Category("[Action] CreateEmployee")]
        public void CreateEmployee_Inputs_NotNullCreateObj_Returns_CreatedAtRouteResult_Include_Object_Has_ValidDataType()
        {
            // Inputs for Act
            var creationDataObj = _fakeDataManager.Employee.GenerateCreationDataObj();

            // Arrange
            var newlyCreatedEmployee = _fakeDataManager.Employee.GenerateEmployeeNo2();

            Mocking_Employee_Create(
                creationDto: creationDataObj,
                returnedEmployeeDto: newlyCreatedEmployee
            );

            // Act
            var actionResult = Act_CreateEmployee(
                creationDto: creationDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(CreatedAtRouteResult)));

            var actualObj = ((CreatedAtRouteResult)actionResult).Value;
            Assert.That(actualObj, Is.Not.Null);
            Assert.That(actualObj, Is.InstanceOf<EmployeeDto>());
        }

        [Test]
        [Category("[Action] CreateEmployee")]
        public void CreateEmployee_Inputs_NotNullCreateObj_Returns_CreatedAtRouteResult_Include_Object_Is_ReturnedFromService()
        {
            // Inputs for Act
            var creationDataObj = _fakeDataManager.Employee.GenerateCreationDataObj();

            // Arrange
            var newlyCreatedEmployee = _fakeDataManager.Employee.GenerateEmployeeNo2();

            Mocking_Employee_Create(
                creationDto: creationDataObj,
                returnedEmployeeDto: newlyCreatedEmployee
            );

            // Act
            var actionResult = Act_CreateEmployee(
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

        #region UpdateEmployeeFully

        private void Mocking_Employee_IsValidId(Guid employeeId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.IsValidId(employeeId))
                .Returns(returnedResult);
        }

        private void Mocking_Employee_UpdateFully(Guid employeeId,
                                                 EmployeeForUpdateDto updateDto,
                                                 bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.UpdateFully(employeeId, updateDto))
                .Returns(returnedResult);
        }

        private IActionResult Act_UpdateEmployeeFully(Guid employeeId, EmployeeForUpdateDto? updateDto)
        {
            return _mockEmployeeController.UpdateEmployeeFully(employeeId, updateDto);
        }

        [Test]
        [Category("[Action] UpdateEmployeeFully")]
        public void UpdateEmployeeFully_Inputs_NonExistingEmployeeId_And_OtherExpectedInputs_Returns_NotFoundObjResult()
        {
            // Inputs for Act
            Guid nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();
            var updateDataObj = _fakeDataManager.Employee.GenerateUpdateDataObj();

            // Arrange
            Mocking_Employee_IsValidId(
                employeeId: nonExistingEmployeeId,
                returnedResult: false
            );

            // Act
            var actionResult = Act_UpdateEmployeeFully(
                employeeId: nonExistingEmployeeId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NotFoundObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFully")]
        public void UpdateEmployeeFully_Inputs_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            EmployeeForUpdateDto? updateDataObj = null;
            Guid existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;

            // Arrange
            Mocking_Employee_IsValidId(
                employeeId: existingEmployeeId,
                returnedResult: true
            );

            // Act
            var actionResult = Act_UpdateEmployeeFully(
                employeeId: existingEmployeeId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateEmployeeFully")]
        public void UpdateEmployeeFully_Inputs_ExpectedData_Returns_NoContentResult()
        {
            // Inputs for Act
            Guid existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;
            var updateDataObj = _fakeDataManager.Employee.GenerateUpdateDataObj();

            // Arrange
            Mocking_Employee_IsValidId(
                employeeId: existingEmployeeId,
                returnedResult: true
            );
            Mocking_Employee_UpdateFully(
                employeeId: existingEmployeeId,
                updateDto: updateDataObj,
                returnedResult: true
            );

            // Act
            var actionResult = Act_UpdateEmployeeFully(
                employeeId: existingEmployeeId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NoContentResult)));
        }

        #endregion

        #region DeleteEmployeeSoftly

        private void Mocking_Employee_DeleteSoftly(Guid employeeId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.DeleteSoftly(employeeId))
                .Returns(returnedResult);
        }

        private IActionResult Act_DeleteEmployeeSoftly(Guid employeeId)
        {
            return _mockEmployeeController.DeleteEmployeeSoftly(employeeId);
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftly")]
        public void DeleteEmployeeSoftly_Inputs_NonExistingEmployeeId_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            var nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();

            // Arrange
            Mocking_Employee_DeleteSoftly(
                employeeId: nonExistingEmployeeId,
                returnedResult: false
            );

            // Act
            var actionResult = Act_DeleteEmployeeSoftly(nonExistingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        [Category("[Action] DeleteEmployeeSoftly")]
        public void DeleteEmployeeSoftly_Inputs_ExistingEmployeeId_Returns_NoContentObjResult()
        {
            // Inputs for Act
            var existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;

            // Arrange
            Mocking_Employee_DeleteSoftly(
                employeeId: existingEmployeeId,
                returnedResult: true
            );

            // Act
            var actionResult = Act_DeleteEmployeeSoftly(existingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NoContentResult>());
        }

        #endregion

        #region DeleteEmployeeHard

        private void Mocking_Employee_DeleteHard(Guid employeeId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Employee.DeleteHard(employeeId))
                .Returns(returnedResult);
        }

        private IActionResult Act_DeleteEmployeeHard(Guid employeeId)
        {
            return _mockEmployeeController.DeleteEmployeeHard(employeeId);
        }

        [Test]
        [Category("[Action] DeleteEmployeeHard")]
        public void DeleteEmployeeHard_Inputs_NonExistingEmployeeId_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            var nonExistingEmployeeId = _fakeDataManager.Employee.GenerateNonExistingEmployeeId();

            // Arrange
            Mocking_Employee_DeleteHard(
                employeeId: nonExistingEmployeeId,
                returnedResult: false
            );

            // Act
            var actionResult = Act_DeleteEmployeeHard(nonExistingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        [Category("[Action] DeleteEmployeeHard")]
        public void DeleteEmployeeHard_Inputs_ExistingEmployeeId_Returns_NoContentObjResult()
        {
            // Inputs for Act
            var existingEmployeeId = _fakeDataManager.Employee.GenerateEmployeeNo1().Id;

            // Arrange
            Mocking_Employee_DeleteHard(
                employeeId: existingEmployeeId,
                returnedResult: true
            );

            // Act
            var actionResult = Act_DeleteEmployeeHard(existingEmployeeId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NoContentResult>());
        }

        #endregion

    }
}
