namespace UnitTest.eShopOnlineApiRestful.Controllers
{
    [TestOf(typeof(CompanyController))]
    [TestFixture]
    public sealed class CompanyControllerTest
    {
        private readonly CompanyController _mockCompanyController;
        private readonly Mock<IServiceManager> _stubServices;

        public CompanyControllerTest() 
        {
            // Strict - Mocking ENOUGH the needed methods 
            _stubServices = new Mock<IServiceManager>(MockBehavior.Strict);

            var _stubControllerParams = new Mock<ControllerParams>(_stubServices.Object);
            var _stubLogger = Mock.Of<ILogger<CompanyController>>();    // instead of "A.Object"
            _mockCompanyController = new CompanyController(_stubLogger, _stubControllerParams.Object);
        }

        [TearDown]
        public void ClearConfiguration()
        {
            _stubServices.Reset();  // Clear ALL setups AFTER each TestCase
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
            return _mockCompanyController!.GetAllCompanies();
        }

        [Test]
        [Category("[Action] GetAllCompanies")]
        public void GetAllCompanies_Input_Parameterless_Returns_OkObjectResult()
        {
            // TestData
            var companiesList = FakeCompany.ListOfCompanies;
            // Arrange
            Mock_Company_GetAll(returnedCompanies: companiesList);

            // Act
            var actionResult = Act_GetAllCompanies();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetAllCompanies")]
        public void GetAllCompanies_Input_Parameterless_Returns_OkObjectResult_With_ValidDataType()
        {
            // TestData
            var companiesList = FakeCompany.ListOfCompanies;

            // Arrange
            Mock_Company_GetAll(returnedCompanies: companiesList);

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
        public void GetAllCompanies_Input_Parameterless_Returns_OkObjectResult_With_AllOfDataRecords()
        {
            // TestData
            var companiesList = FakeCompany.ListOfCompanies;
            var companiesListForComparision = FakeCompany.ListOfCompanies;

            // Arrange
            Mock_Company_GetAll(returnedCompanies: companiesList);

            // Act
            var actionResult = Act_GetAllCompanies();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var okObjectResult = (OkObjectResult)actionResult;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<CompanyDto>>());

            var actualCompaniesList = (IEnumerable<CompanyDto>)okObjectResult.Value;
            Assert.That(
                actualCompaniesList, Is.EquivalentTo(companiesListForComparision),
                "GetAllCompanies() returns an insufficient number of IEnumerable<CompanyDto> records."
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
        public void GetCompanyById_Input_ExistingCompanyId_Returns_OkObjectResult()
        {
            // TestData
            Guid existingCompanyId = FakeCompany.CompanyNo1.Id;
            var existingCompanyObj = FakeCompany.CompanyNo1;

            // Arrange
            Mocking_Company_GetById(
                companyId: existingCompanyId,
                returnedCompany: existingCompanyObj
            );

            // Act
            var actionResult = Act_GetCompanyById(companyId: existingCompanyId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [Category("[Action] GetCompanyById")]
        public void GetCompanyById_Input_ExistingCompanyId_Returns_OkObjectResult_With_ExpectedDataType()
        {
            // TestData
            Guid existingCompanyId = FakeCompany.CompanyNo1.Id;
            var existingCompanyObj = FakeCompany.CompanyNo1;

            // Arrange
            Mocking_Company_GetById(
                companyId: existingCompanyId,
                returnedCompany: existingCompanyObj
            );

            // Act
            var actionResult = Act_GetCompanyById(companyId: existingCompanyId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            var actualData = ((OkObjectResult) actionResult).Value;
            Assert.That(actualData, Is.Not.Null);
            Assert.That(actualData, Is.InstanceOf<CompanyDto>());
        }

        [Test]
        [Category("[Action] GetCompanyById")]
        public void GetCompanyById_Input_NonExistingCompanyId_Returns_NotFoundObjectResult_With_Message()
        {
            // TestData
            Guid nonExistingCompanyId = FakeCompany.NonExistingCompanyId;

            // Arrange
            Mocking_Company_GetById(
                companyId: nonExistingCompanyId,
                returnedCompany: null
            );

            // Act
            var actionResult = Act_GetCompanyById(companyId: nonExistingCompanyId);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<NotFoundObjectResult>());

            var actualData = ((NotFoundObjectResult) actionResult).Value;
            Assert.That(actualData, Is.Not.Null);
            Assert.That(actualData, Is.InstanceOf<string>());
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
        public void UpdateCompanyFully_Input_NonExistingCompanyId_And_OtherExpectedInputs_Returns_NotFoundObjResult()
        {
            // TestData
            Guid nonExistingCompanyId = FakeCompany.NonExistingCompanyId;
            bool validationResult = false;
            /// Other expected inputs
            CompanyForUpdateDto? updateObj = FakeCompany.UpdateObject;

            // Arrange
            Mocking_Company_IsValidId(
                companyId: nonExistingCompanyId, 
                returnedResult: validationResult
            );

            // Act
            var actionResult = Act_UpdateCompanyFully(
                companyId: nonExistingCompanyId, 
                updateDto: updateObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NotFoundObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateCompanyFully")]
        public void UpdateCompanyFully_Input_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // TestData
            CompanyForUpdateDto? updateObj = null;
            /// Other expected inputs
            Guid existingCompanyId = FakeCompany.CompanyNo1.Id;
            bool validationResult = true;

            // Arrange
            Mocking_Company_IsValidId(
                companyId: existingCompanyId,
                returnedResult: validationResult
            );

            // Act
            var actionResult = Act_UpdateCompanyFully(
                companyId: existingCompanyId,
                updateDto: updateObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }

        [Test]
        [Category("[Action] UpdateCompanyFully")]
        public void UpdateCompanyFully_Input_ExpectedData_Returns_NoContentResult()
        {
            // TestData
            Guid existingCompanyId = FakeCompany.CompanyNo1.Id;
            CompanyForUpdateDto? updateObj = FakeCompany.UpdateObject;
            bool resultIsValidIdFunc = true;
            bool resultUpdateFullyFunc = true;

            // Arrange
            Mocking_Company_IsValidId(
                companyId: existingCompanyId,
                returnedResult: resultIsValidIdFunc
            );
            Mocking_Company_UpdateFully(
                companyId: existingCompanyId,
                updateDto: updateObj,
                returnedResult: resultUpdateFullyFunc
            );

            // Act
            var actionResult = Act_UpdateCompanyFully(
                companyId: existingCompanyId,
                updateDto: updateObj
            );

            // Assert
            Assert.That(actionResult, Is.InstanceOf(typeof(NoContentResult)));
        }

        #endregion

    }
}
