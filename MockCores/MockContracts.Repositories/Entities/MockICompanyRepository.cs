namespace MockContracts.Repositories.Entities
{
    internal sealed class MockICompanyRepository
    {
        public static Mock<ICompanyRepository> GetInstance()
        {
            // Fake Data
            var fakeDataForCompany = new FakeDataForCompany();
            Guid nonExistingCompanyId = fakeDataForCompany.GetNonExistingCompanyId();
            var listOfCompanies = fakeDataForCompany.GetListOfCompanies();

            // SetUps
            var mockCompanyRepo = new Mock<ICompanyRepository>(MockBehavior.Strict);

            mockCompanyRepo
                .Setup(s => s.GetAllAsync(It.IsAny<bool>()))
                .ReturnsAsync(listOfCompanies);

            mockCompanyRepo
                .Setup(s => s.GetByIdAsync(It.IsAny<bool>(), It.IsAny<Guid>()))
                .ReturnsAsync((bool isTrackChanges, Guid companyId) => listOfCompanies.FirstOrDefault(c => c.Id == companyId));

            mockCompanyRepo
                .Setup(s => s.IsValidIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid companyId) => companyId != nonExistingCompanyId);

            return mockCompanyRepo;
        }
    }
}
