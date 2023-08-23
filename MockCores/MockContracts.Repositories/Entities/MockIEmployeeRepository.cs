namespace MockContracts.Repositories.Entities
{
    internal sealed class MockIEmployeeRepository
    {
        public static Mock<IEmployeeRepository> GetInstance()
        {
            // Fake Data
            var fakeDataForEmployee = new FakeDataForEmployee();
            Guid nonExistingEmployeeId = fakeDataForEmployee.GetNonExistingEmployeeId();
            var listOfEmployees = fakeDataForEmployee.GetListOfEmployees();

            // SetUps
            var mockEmployeeRepo = new Mock<IEmployeeRepository>(MockBehavior.Strict);

            mockEmployeeRepo
                .Setup(s => s.GetAllAsync(It.IsAny<bool>()))
                .ReturnsAsync(listOfEmployees);

            mockEmployeeRepo
                .Setup(s => s.GetByIdAsync(It.IsAny<bool>(), It.IsAny<Guid>()))
                
                .ReturnsAsync((bool isTrackChanges, Guid employeeId) 
                            => listOfEmployees.FirstOrDefault(e => e.Id == employeeId));

            mockEmployeeRepo
                .Setup(s => s.IsValidIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(((Guid employeeId) => employeeId != nonExistingEmployeeId));

            mockEmployeeRepo
                .Setup(s => s.Create(It.IsAny<Employee>()))
                .Callback(() => { return; });   // void method

            mockEmployeeRepo
                .Setup(s => s.DeleteSoftly(It.IsAny<Employee>()))
                .Callback(() => { return; });   // void method

            mockEmployeeRepo
                .Setup(s => s.DeleteHard(It.IsAny<Employee>()))
                .Callback(() => { return; });   // void method

            return mockEmployeeRepo;
        }
    }
}
