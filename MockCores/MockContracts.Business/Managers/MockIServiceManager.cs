using Contracts.Business.Managers;
using MockContracts.Business.Entities;

namespace MockContracts.Business.Managers
{
    public class MockIServiceManager
    {
        public static Mock<IServiceManager> GetInstance()
        {
            // Entity Services
            var mockCompanyService = MockICompanyService.GetInstance();
            var mockEmployeeService = MockIEmployeeService.GetInstance();

            // SetUps
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager
                .Setup(s => s.Company)
                .Returns(mockCompanyService.Object);

            mockServiceManager
                .Setup(s => s.Employee)
                .Returns(mockEmployeeService.Object);

            return mockServiceManager;
        }
    }
}
