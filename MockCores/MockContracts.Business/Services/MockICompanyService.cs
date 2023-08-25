namespace MockContracts.Business.Services
{
    internal sealed class MockICompanyService
    {
        public static Mock<ICompanyService> GetInstance()
        {
            var mockCompanyService = new Mock<ICompanyService>(MockBehavior.Strict);

            // Fake Data
            Guid nonExistingCompanyId = FakeDataForCompany.GetNonExistingCompanyId();
            FakeDataForCompany fakeDataForCompany = new FakeDataForCompany();
            IEnumerable<CompanyDto> listOfCompanyDtos = fakeDataForCompany.GetListOfCompanyDtos();
            CompanyForUpdateDto invalidUpdateDto = fakeDataForCompany.GetInvalidUpdateDto();

            // SetUps
            mockCompanyService
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid companyId) => listOfCompanyDtos.FirstOrDefault(c => c.Id == companyId));

            mockCompanyService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(listOfCompanyDtos);

            mockCompanyService
                .Setup(s => s.IsValidIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid companyId) => companyId != nonExistingCompanyId);

            mockCompanyService
                .Setup(s => s.UpdateFullyAsync(It.IsAny<Guid>(), It.IsAny<CompanyForUpdateDto>()))
                .ReturnsAsync((Guid companyId, CompanyForUpdateDto updateDto) =>
                {
                    return (companyId != nonExistingCompanyId
                         && updateDto.Name != invalidUpdateDto.Name);
                });

            return mockCompanyService;
        }
    }
}
