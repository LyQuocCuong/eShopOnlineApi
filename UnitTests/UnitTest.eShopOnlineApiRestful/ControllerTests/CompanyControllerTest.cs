﻿namespace UnitTest.eShopOnlineApiRestful.ControllerTests
{
    [TestFixture]
    [TestOf(typeof(CompanyController))]
    public sealed class CompanyControllerTest : AbstractControllerTest<CompanyController>
    {
        protected override CompanyController InitController()
        {
            return new CompanyController(base.GetILogger(), base.GetControllerParams());
        }

        #region GetAllCompaniesAsync

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult()
        {
            // Arrange
            var companyController = InitController();

            // Act
            var okObjResult = await companyController.GetAllCompaniesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Of_CompanyDto()
        {
            // Arrange
            var companyController = InitController();

            // Act
            var okObjResult = await companyController.GetAllCompaniesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.Value, Is.InstanceOf<IEnumerable<CompanyDto>>());
        }

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_FullAmountItems()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var expectedCollectionData = fakeDataForCompany.GetListOfCompanyDtos();
            var companyController = InitController();

            // Act
            var okObjResult = await companyController.GetAllCompaniesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            var collectionData = okObjResult.Value as IEnumerable<CompanyDto>;
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Count(), Is.EqualTo(expectedCollectionData.Count()),
                "GetAllCompanies() returns an insufficient number of IEnumerable<CompanyDto> records."
            );
        }

        [Test]
        [Category("[Action] GetAllCompaniesAsync")]
        public async Task GetAllCompaniesAsync_Inputs_Parameterless_Returns_OkObjectResult_Include_Collection_Has_UniqueCompanyIds()
        {
            // Arrange
            var companyController = InitController();

            // Act
            var okObjResult = await companyController.GetAllCompaniesAsync() as OkObjectResult;

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            var collectionData = okObjResult.Value as IEnumerable<CompanyDto>;
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Select(c => c.Id), Is.Unique,
                "Collection data returned from GetAllCompanies() is DUPLICATED in the CompanyId field."
            );
        }

        #endregion

        #region GetCompanyByIdAsync

        [Test]
        [Category("[Action] GetCompanyByIdAsync")]
        public async Task GetCompanyByIdAsync_Inputs_ExistingCompanyId_Returns_OkObjectResult()
        {
            // Arrange
            var existingCompanyId = FakeDataForCompany.GetNormalCompanyId();
            var companyController = InitController();

            // Act
            var okObjResult = (OkObjectResult)await companyController.GetCompanyByIdAsync(existingCompanyId);

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        [Category("[Action] GetCompanyByIdAsync")]
        public async Task GetCompanyByIdAsync_Inputs_ExistingCompanyId_Returns_OkObjectResult_Include_Object_Is_CompanyDto()
        {
            // Arrange
            var existingCompanyId = FakeDataForCompany.GetNormalCompanyId();
            var companyController = InitController();

            // Act
            var okObjResult = (OkObjectResult)await companyController.GetCompanyByIdAsync(existingCompanyId);

            // Assert
            Assert.That(okObjResult, Is.Not.Null);
            Assert.That(okObjResult.Value, Is.InstanceOf<CompanyDto>());
        }

        [Test]
        [Category("[Action] GetCompanyByIdAsync")]
        public async Task GetCompanyByIdAsync_Inputs_NonExistingCompanyId_Returns_NotFoundObjectResult()
        {
            // Arrange
            var nonExistingCompanyId = FakeDataForCompany.GetNonExistingCompanyId();
            var companyController = InitController();

            // Act
            var notFoundObjResult = (NotFoundObjectResult)await companyController.GetCompanyByIdAsync(nonExistingCompanyId);

            // Assert
            Assert.That(notFoundObjResult, Is.Not.Null);
            Assert.That(notFoundObjResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        #endregion

        #region UpdateCompanyFullyAsync

        [Test]
        [Category("[Action] UpdateCompanyFullyAsync")]
        public async Task UpdateCompanyFullyAsync_Inputs_NonExistingCompanyId_And_OtherExpectedInputs_Returns_NotFoundObjResult()
        {
            // Arrange
            var nonExistingCompanyId = FakeDataForCompany.GetNonExistingCompanyId();
            var fakeDataForCompany = new FakeDataForCompany();
            var validUpdateDto = fakeDataForCompany.GetValidUpdateDto();
            var companyController = InitController();

            // Act
            var notFoundObjResult = await companyController.UpdateCompanyFullyAsync(nonExistingCompanyId, validUpdateDto) as NotFoundObjectResult;

            // Assert
            Assert.That(notFoundObjResult, Is.Not.Null);
            Assert.That(notFoundObjResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        [Category("[Action] UpdateCompanyFullyAsync")]
        public async Task UpdateCompanyFullyAsync_Inputs_NullUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Arrange
            var existingCompanyId = FakeDataForCompany.GetNormalCompanyId();
            var companyController = InitController();

            // Act
            var badRequestObjResult = await companyController.UpdateCompanyFullyAsync(existingCompanyId, null) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] UpdateCompanyFullyAsync")]
        public async Task UpdateCompanyFullyAsync_Inputs_InvalidUpdateObj_And_OtherExpectedInputs_Returns_BadRequestObjResult()
        {
            // Arrange
            var existingCompanyId = FakeDataForCompany.GetNormalCompanyId();
            var fakeDataForCompany = new FakeDataForCompany();
            var invalidUpdateDto = fakeDataForCompany.GetInvalidUpdateDto();
            var companyController = InitController();

            // Act
            var badRequestObjResult = await companyController.UpdateCompanyFullyAsync(existingCompanyId, invalidUpdateDto) as BadRequestObjectResult;

            // Assert
            Assert.That(badRequestObjResult, Is.Not.Null);
            Assert.That(badRequestObjResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        [Category("[Action] UpdateCompanyFullyAsync")]
        public async Task UpdateCompanyFullyAsync_Inputs_ExpectedData_Returns_NoContentResult()
        {
            // Arrange
            var existingCompanyId = FakeDataForCompany.GetNormalCompanyId();
            var fakeDataForCompany = new FakeDataForCompany();
            var validUpdateDto = fakeDataForCompany.GetValidUpdateDto();
            var companyController = InitController();

            // Act
            var noContentResult = await companyController.UpdateCompanyFullyAsync(existingCompanyId, validUpdateDto) as NoContentResult;

            // Assert
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }

        #endregion

    }
}
