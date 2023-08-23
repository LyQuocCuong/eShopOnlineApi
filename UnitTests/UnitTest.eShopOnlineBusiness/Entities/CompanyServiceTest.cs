namespace UnitTest.eShopOnlineBusiness.Entities
{
    [TestFixture]
    [TestOf(typeof(CompanyService))]
    public sealed class CompanyServiceTest : AbstractServiceTest<CompanyService>
    {
        protected override CompanyService InitService()
        {
            return new CompanyService(base.GetILogger(), base.GetServiceParams());
        }

        #region GetAllAsync

        [Test]
        [Category("[Method] GetAllAsync")]
        public async Task GetAllAsync_Inputs_Parameterless_Returns_Collection_Of_CompanyDto()
        {
            // Arrange
            var companyService = InitService();

            // Act
            var result = await companyService.GetAllAsync() as IEnumerable<CompanyDto>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [Category("[Method] GetAllAsync")]
        public async Task GetAllAsync_Inputs_Parameterless_Returns_Collection_Has_FullAmountItems()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var expectedCollectionData = fakeDataForCompany.GetListOfCompanies();
            var companyService = InitService();

            // Act
            var collectionData = await companyService.GetAllAsync() as IEnumerable<CompanyDto> ;

            // Assert
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Count(), Is.EqualTo(expectedCollectionData.Count()),
                "GetAllAsync() returns an insufficient number of IEnumerable<CompanyDto> records."
            );
        }

        [Test]
        [Category("[Method] GetAllAsync")]
        public async Task GetAllAsync_Inputs_Parameterless_Returns_Collection_Has_UniqueCompanyIds()
        {
            // Arrange
            var companyService = InitService();

            // Act
            var collectionData = await companyService.GetAllAsync() as IEnumerable<CompanyDto>;

            // Assert
            Assert.That(collectionData, Is.Not.Null);
            Assert.That(
                collectionData.Select(c => c.Id), Is.Unique,
                "Collection data returned from GetAllAsync() is DUPLICATED in the CompanyId field."
            );
        }

        #endregion

        #region GetByIdAsync

        [Test]
        [Category("[Method] GetByIdAsync")]
        public async Task GetByIdAsync_Inputs_ExistingCompanyId_Returns_Object_Is_CompanyDto()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var existingCompanyId = fakeDataForCompany.GetExistingCompanyId();
            var companyService = InitService();

            // Act
            var result = await companyService.GetByIdAsync(existingCompanyId) as CompanyDto;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [Category("[Method] GetByIdAsync")]
        public async Task GetByIdAsync_Inputs_NonExistingCompanyId_Returns_Null()
        {
            // Arrange
            var fakeDataForEmployee = new FakeDataForCompany();
            var nonExistingCompanyId = fakeDataForEmployee.GetNonExistingCompanyId();
            var companyService = InitService();

            // Act
            var result = await companyService.GetByIdAsync(nonExistingCompanyId);

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        #region IsValidIdAsync

        [Test]
        [Category("[Method] IsValidIdAsync")]
        public async Task IsValidIdAsync_Inputs_ExistingCompanyId_Returns_True()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var existingCompanyId = fakeDataForCompany.GetExistingCompanyId();
            var companyService = InitService();

            // Act
            var result = await companyService.IsValidIdAsync(existingCompanyId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("[Method] IsValidIdAsync")]
        public async Task IsValidIdAsync_Inputs_NonExistingCompanyId_Returns_True()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var nonExistingCompanyId = fakeDataForCompany.GetNonExistingCompanyId();
            var companyService = InitService();

            // Act
            var result = await companyService.IsValidIdAsync(nonExistingCompanyId);

            // Assert
            Assert.That(result, Is.False);
        }

        #endregion

        #region UpdateFullyAsync

        [Test]
        [Category("[Method] UpdateFullyAsync")]
        public async Task UpdateFullyAsync_Inputs_NonExistingCompanyId_And_OtherExpectedInputs_Returns_False()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var nonExistingCompanyId = fakeDataForCompany.GetNonExistingCompanyId();
            var validUpdateDto = fakeDataForCompany.GetValidUpdateDto();
            var companyService = InitService();

            // Act
            var result = await companyService.UpdateFullyAsync(nonExistingCompanyId, validUpdateDto);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("[Optimize Later]")]
        [Category("[Method] UpdateFullyAsync")]
        public async Task UpdateFullyAsync_Inputs_InvalidUpdateObj_And_OtherExpectedInputs_Returns_False()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var existingCompanyId = fakeDataForCompany.GetExistingCompanyId();
            var invalidUpdateDto = fakeDataForCompany.GetInvalidUpdateDto();
            var companyService = InitService();

            // Act
            var result = await companyService.UpdateFullyAsync(existingCompanyId, invalidUpdateDto);

            // Assert
            Assert.That(result, Is.True);   // expected: Is.False
        }

        [Test]
        [Category("[Method] UpdateFullyAsync")]
        public async Task UpdateFullyAsync_Inputs_ExpectedData_Returns_True()
        {
            // Arrange
            var fakeDataForCompany = new FakeDataForCompany();
            var existingCompanyId = fakeDataForCompany.GetExistingCompanyId();
            var validUpdateDto = fakeDataForCompany.GetValidUpdateDto();
            var companyService = InitService();

            // Act
            var result = await companyService.UpdateFullyAsync(existingCompanyId, validUpdateDto);

            // Assert
            Assert.That(result, Is.True);
        }

        #endregion

    }
}
