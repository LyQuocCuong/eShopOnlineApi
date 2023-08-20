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

        #region GetAllCompanies

        private void Mock_Company_GetAll(IEnumerable<CompanyDto> returnedCompanies)
        {
            _stubServices
                .Setup(s => s.Company.GetAll())
                .Returns(returnedCompanies);
        }

        private IActionResult Act_GetAllCompanies()
        {
            return _mockCompanyController.GetAllCompanies();
        }

        [Test]
        [Category("[Action] GetAllCompanies")]
        public void GetAllCompanies_Inputs_Parameterless_Returns_OkObjectResult()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Arrange
            Mock_Company_GetAll(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = Act_GetAllCompanies();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetAllCompanies")]
        public void GetAllCompanies_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_ValidDataType()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Arrange
            Mock_Company_GetAll(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = Act_GetAllCompanies();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjectResult = (OkObjectResult)actionResult;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<CompanyDto>>());
        }

        [Test]
        [Category("[Action] GetAllCompanies")]
        public void GetAllCompanies_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_SameAmountItems()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Data for Assertion
            var expectedAmountItems = listOfCompanies.Count();

            // Arrange
            Mock_Company_GetAll(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = Act_GetAllCompanies();

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
        [Category("[Action] GetAllCompanies")]
        public void GetAllCompanies_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_UniqueCompanyIds()
        {
            // Inputs for Act
            var listOfCompanies = _fakeDataManager.Company.GenerateAListOfCompanies();

            // Arrange
            Mock_Company_GetAll(returnedCompanies: listOfCompanies);

            // Act
            var actionResult = Act_GetAllCompanies();

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

        #region GetCompanyById

        private void Mocking_Company_GetById(Guid companyId, CompanyDto? returnedCompany)
        {
            _stubServices
                .Setup(s => s.Company.GetById(companyId))
                .Returns(returnedCompany);
        }

        private IActionResult Act_GetCompanyById(Guid companyId)
        {
            return _mockCompanyController.GetCompanyById(companyId);
        }

        [Test]
        [Category("[Action] GetCompanyById")]
        public void GetCompanyById_Inputs_ExistingCompanyId_Returns_OkObjectResult()
        {
            // Inputs for Act
            var existingCompany = _fakeDataManager.Company.GenerateCompanyNo1();

            // Arrange
            Mocking_Company_GetById(
                companyId: existingCompany.Id,
                returnedCompany: existingCompany
            );

            // Act
            var actionResult = Act_GetCompanyById(companyId: existingCompany.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetCompanyById")]
        public void GetCompanyById_Inputs_ExistingCompanyId_Returns_OkObjectResult_Include_Object_Has_ValidDataType()
        {
            // Inputs for Act
            var existingCompany = _fakeDataManager.Company.GenerateCompanyNo1();

            // Arrange
            Mocking_Company_GetById(
                companyId: existingCompany.Id,
                returnedCompany: existingCompany
            );

            // Act
            var actionResult = Act_GetCompanyById(companyId: existingCompany.Id);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var actualData = ((OkObjectResult) actionResult).Value;
            Assert.That(actualData, Is.Not.Null);
            Assert.That(actualData, Is.InstanceOf<CompanyDto>());
        }

        [Test]
        [Category("[Action] GetCompanyById")]
        public void GetCompanyById_Inputs_NonExistingCompanyId_Returns_NotFoundObjectResult_Include_MessageString()
        {
            // Inputs for Act
            Guid nonExistingCompanyId = _fakeDataManager.Company.GenerateNonExistingCompanyId();

            // Arrange
            Mocking_Company_GetById(
                companyId: nonExistingCompanyId,
                returnedCompany: null
            );

            // Act
            var actionResult = Act_GetCompanyById(companyId: nonExistingCompanyId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NotFoundObjectResult>());

            var message = ((NotFoundObjectResult) actionResult).Value;
            Assert.That(message, Is.Not.Null);
            Assert.That(message, Is.InstanceOf<string>());
        }

        #endregion

        #region UpdateCompanyFully

        private void Mocking_Company_IsValidId(Guid companyId, bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Company.IsValidId(companyId))
                .Returns(returnedResult);
        }

        private void Mocking_Company_UpdateFully(Guid companyId, 
                                                 CompanyForUpdateDto updateDto,
                                                 bool returnedResult)
        {
            _stubServices
                .Setup(s => s.Company.UpdateFully(companyId, updateDto))
                .Returns(returnedResult);
        }

        private IActionResult Act_UpdateCompanyFully(Guid companyId, CompanyForUpdateDto? updateDto)
        {
            return _mockCompanyController.UpdateCompanyFully(companyId, updateDto);
        }

        [Test]
        [Category("[Action] UpdateCompanyFully")]
        public void UpdateCompanyFully_Inputs_NonExistingCompanyId_And_OtherExpectedInputs_Returns_NotFoundObjResult()
        {
            // Inputs for Act
            Guid nonExistingCompanyId = _fakeDataManager.Company.GenerateNonExistingCompanyId();
            var updateDataObj = _fakeDataManager.Company.GenerateUpdateDataObj();

            // Arrange
            Mocking_Company_IsValidId(
                companyId: nonExistingCompanyId, 
                returnedResult: false
            );

            // Act
            var actionResult = Act_UpdateCompanyFully(
                companyId: nonExistingCompanyId, 
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NotFoundObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateCompanyFully")]
        public void UpdateCompanyFully_Inputs_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Inputs for Act
            Guid existingCompanyId = _fakeDataManager.Company.GenerateCompanyNo1().Id;
            CompanyForUpdateDto? updateDataObj = null;

            // Arrange
            Mocking_Company_IsValidId(
                companyId: existingCompanyId,
                returnedResult: true
            );

            // Act
            var actionResult = Act_UpdateCompanyFully(
                companyId: existingCompanyId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateCompanyFully")]
        public void UpdateCompanyFully_Inputs_ExpectedData_Returns_NoContentResult()
        {
            // Inputs for Act
            Guid existingCompanyId = _fakeDataManager.Company.GenerateCompanyNo1().Id;
            CompanyForUpdateDto? updateDataObj = _fakeDataManager.Company.GenerateUpdateDataObj();

            // Arrange
            Mocking_Company_IsValidId(
                companyId: existingCompanyId,
                returnedResult: true
            );
            Mocking_Company_UpdateFully(
                companyId: existingCompanyId,
                updateDto: updateDataObj,
                returnedResult: true
            );

            // Act
            var actionResult = Act_UpdateCompanyFully(
                companyId: existingCompanyId,
                updateDto: updateDataObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NoContentResult)));
        }

        #endregion

    }
}
