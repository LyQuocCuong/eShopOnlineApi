using Contracts.Repositories.Managers;
using MockContracts.Repositories.Entities;

namespace MockContracts.Repositories.Managers
{
    public sealed class MockIRepositoryManager
    {
        public static Mock<IRepositoryManager> GetInstance()
        {
            // Entity Repos
            var mockCompanyRepo = MockICompanyRepository.GetInstance();
            var mockEmployeeRepo = MockIEmployeeRepository.GetInstance();

            // SetUps
            var mockRepoManager = new Mock<IRepositoryManager>(MockBehavior.Strict);

            mockRepoManager
                .Setup(s => s.SaveChangesAsync())
                .Returns(Task.CompletedTask);   // Task of (void) method

            mockRepoManager
                .Setup(s => s.Company)
                .Returns(mockCompanyRepo.Object);

            mockRepoManager
                .Setup(s => s.Employee)
                .Returns(mockEmployeeRepo.Object);

            return mockRepoManager;
        }
    }
}
