namespace UnitTest.eShopOnlineApiRestful.Controllers
{
    [TestFixture]
    [TestOf(typeof(CompanyController))]
    public sealed class CompanyControllerTest : AbstractControllerTest<CompanyController>
    {
        private CompanyController _mockCompanyController;

        public override void SetUpBeforeExecutingEachTest()
        {
            _mockCompanyController = new CompanyController(_stubLogger.Object,
                                                           _stubControllerParams.Object);
        }

        #region GetAllCompaniesAsync

        private void Mock_Company_GetAllAsync(IEnumerable<CompanyDto> returnedCompanies)
        {
            _stubServices
                .Setup(s => s.Company.GetAllAsync())
                .ReturnsAsync(returnedCompanies);
        }

        private async Task<IActionResult> Act_GetAllCompaniesAsync()
        {
            return await _mockCompanyController.GetAllCompaniesAsync();
        }

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Arrange
            Mock_Company_GetAllAsync(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = await Act_GetAllCompaniesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_ValidDataType()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Arrange
            Mock_Company_GetAllAsync(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = await Act_GetAllCompaniesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjectResult = (OkObjectResult)actionResult;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<CompanyDto>>());
        }

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_SameAmountItems()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Data for Assertion
            var expectedAmountItems = listOfCompanies.Count();

            // Arrange
            Mock_Company_GetAllAsync(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = await Act_GetAllCompaniesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjectResult = (OkObjectResult)actionResult;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<CompanyDto>>());

            var actualCompaniesCollection = (IEnumerable<CompanyDto>)okObjectResult.Value;
            Assert.That(
                actualCompaniesCollection.Count(), Is.EqualTo(expectedAmountItems),
                "GetAllCompanies() returns an insufficient number of IEnumerable<CompanyDto> records."
            );
        }

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_UniqueCompanyIds()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Arrange
            Mock_Company_GetAllAsync(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = await Act_GetAllCompaniesAsync();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjectResult = (OkObjectResult)actionResult;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<CompanyDto>>());

            var actualCompaniesCollection = (IEnumerable<CompanyDto>)okObjectResult.Value;
            var companyIds = actualCompaniesCollection.Select(c => c.Id);
            Assert.That(
                companyIds, Is.Unique,
                "Collection data returned from GetAllCompanies() is DUPLICATED in the CompanyId field."
            );
        }

        #endregion

        #region GetCompanyByIdAsync

        private void Mocking_Company_GetByIdAsync(Guid companyId, CompanyDto? returnedCompany)
        {
            _stubServices
                .Setup(s => s.Company.GetByIdAsync(companyId))
                .ReturnsAsync(returnedCompany);
        }

        private async Task<IActionResult> Act_GetCompanyByIdAsync(Guid companyId)
        {
            return await _mockCompanyController.GetCompanyByIdAsync(companyId);
        }

        [Test]
        [Category("[Action] GetCompanyByIdAsync")]
        public async Task GetCompanyByIdAsync_Inputs_ExistingCompanyId_Returns_OkObjectResult()
        {
            // Inputs for Act
            var existingCompany = _fakeDataManager.Company.GenerateCompanyNo1();

            // Arrange
            Mocking_Company_GetByIdAsync(
                companyId: existingCompany.Id,
                returnedCompany: existingCompany
            );

            // Act
            var actionResult = await Act_GetCompanyByIdAsync(companyId: existingCompany.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetCompanyByIdAsync")]
        public async Task GetCompanyByIdAsync_Inputs_ExistingCompanyId_Returns_OkObjectResult_Include_Object_Has_ValidDataType()
        {
            // Inputs for Act
            var existingCompany = _fakeDataManager.Company.GenerateCompanyNo1();

            // Arrange
            Mocking_Company_GetByIdAsync(
                companyId: existingCompany.Id,
                returnedCompany: existingCompany
            );

            // Act
            var actionResult = await Act_GetCompanyByIdAsync(companyId: existingCompany.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var actualData = ((OkObjectResult) actionResult).Value;
            Assert.That(actualData, Is.Not.Null);
            Assert.That(actualData, Is.InstanceOf<CompanyDto>());
        }

        [Test]
        [Category("[Action] GetCompanyByIdAsync")]
        public async Task GetCompanyByIdAsync_Inputs_NonExistingCompanyId_Returns_NotFoundObjectResult_Include_MessageString()
        {
            // Inputs for Act
            Guid nonExistingCompanyId = _fakeDataManager.Company.GenerateNonExistingCompanyId();

            // Arrange
            Mocking_Company_GetByIdAsync(
                companyId: nonExistingCompanyId,
                returnedCompany: null
            );

            // Act
            var actionResult = await Act_GetCompanyByIdAsync(companyId: nonExistingCompanyId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NotFoundObjectResult>());

            var message = ((NotFoundObjectResult) actionResult).Value;
            Assert.That(message, Is.Not.Null);
            Assert.That(message, Is.InstanceOf<string>());
        }

        #endregion

        #region UpdateCompanyFullyAsync

        private void Mocking_Company_IsValidIdAsync(Guid companyId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Company.IsValidIdAsync(companyId))
                .ReturnsAsync(returnedResult);
        }

        private void Mocking_Company_UpdateFullyAsync(Guid companyId, 
                                                 CompanyForUpdateDto updateDto,
                                                 bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Company.UpdateFullyAsync(companyId, updateDto))
                .ReturnsAsync(returnedResult);
        }

        private async Task<IActionResult> Act_UpdateCompanyFullyAsync(Guid companyId, CompanyForUpdateDto? updateDto)
        {
            return await _mockCompanyController.UpdateCompanyFullyAsync(companyId, updateDto);
        }

        [Test]
        [Category("[Action] UpdateCompanyFullyAsync")]
        public async Task UpdateCompanyFullyAsync_Inputs_NonExistingCompanyId_And_OtherExpectedInputs_Returns_NotFoundObjResult()
        {
            // Inputs for Act
            Guid nonExistingCompanyId = _fakeDataManager.Company.GenerateNonExistingCompanyId();
            var updateDataObj = _fakeDataManager.Company.GenerateUpdateDataObj();

            // Arrange
            Mocking_Company_IsValidIdAsync(
                companyId: nonExistingCompanyId, 
                returnedResult: false
            );

            // Act
            var actionResult = await Act_UpdateCompanyFullyAsync(
                companyId: nonExistingCompanyId, 
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NotFoundObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateCompanyFullyAsync")]
        public async Task UpdateCompanyFullyAsync_Inputs_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            Guid existingCompanyId = _fakeDataManager.Company.GenerateCompanyNo1().Id;
            CompanyForUpdateDto? updateDataObj = null;

            // Arrange
            Mocking_Company_IsValidIdAsync(
                companyId: existingCompanyId,
                returnedResult: true
            );

            // Act
            var actionResult = await Act_UpdateCompanyFullyAsync(
                companyId: existingCompanyId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateCompanyFullyAsync")]
        public async Task UpdateCompanyFullyAsync_Inputs_ExpectedData_Returns_NoContentResult()
        {
            // Inputs for Act
            Guid existingCompanyId = _fakeDataManager.Company.GenerateCompanyNo1().Id;
            CompanyForUpdateDto? updateDataObj = _fakeDataManager.Company.GenerateUpdateDataObj();

            // Arrange
            Mocking_Company_IsValidIdAsync(
                companyId: existingCompanyId,
                returnedResult: true
            );
            Mocking_Company_UpdateFullyAsync(
                companyId: existingCompanyId,
                updateDto: updateDataObj,
                returnedResult: true
            );

            // Act
            var actionResult = await Act_UpdateCompanyFullyAsync(
                companyId: existingCompanyId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NoContentResult)));
        }

        #endregion

    }
}
