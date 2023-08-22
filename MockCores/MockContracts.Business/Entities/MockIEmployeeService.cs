namespace MockContracts.Business.Entities
{
    internal sealed class MockIEmployeeService
    {
        public static Mock<IEmployeeService> GetInstance()
        {
            var mockEmployeeService = new Mock<IEmployeeService>();

            // Fake Data
            FakeDataForEmployee fakeDataForEmployee = new FakeDataForEmployee();
            Guid nonExistingEmployeeId = fakeDataForEmployee.GetNonExistingEmployeeId();
            Guid unableDeleteEmployeeId = fakeDataForEmployee.GetUnableDeletedEmployeeId();
            IEnumerable<EmployeeDto> listOfEmployeeDtos = fakeDataForEmployee.GetListOfEmployeeDtos();
            EmployeeForUpdateDto invalidUpdateDto = fakeDataForEmployee.GetInvalidUpdateDto();
            EmployeeDto newlyCreatedEmployeeDto = fakeDataForEmployee.GetNewlyCreatedEmployeeDto();

            // SetUps
            mockEmployeeService
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid employeeId) => listOfEmployeeDtos.FirstOrDefault(c => c.Id == employeeId));

            mockEmployeeService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(listOfEmployeeDtos);

            mockEmployeeService
                .Setup(s => s.IsValidIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid employeeId) => employeeId != nonExistingEmployeeId);

            mockEmployeeService
                .Setup(s => s.UpdateFullyAsync(It.IsAny<Guid>(), It.IsAny<EmployeeForUpdateDto>()))
                .ReturnsAsync((Guid companyId, EmployeeForUpdateDto updateDto) =>
                {
                    return (companyId != nonExistingEmployeeId
                         && updateDto.Code != invalidUpdateDto.Code);
                });

            mockEmployeeService
                .Setup(s => s.CreateAsync(It.IsAny<EmployeeForCreationDto>()))
                .ReturnsAsync(newlyCreatedEmployeeDto);

            mockEmployeeService
                .Setup(s => s.DeleteSoftlyAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid employeeId) =>
                {
                    return employeeId != unableDeleteEmployeeId;
                });

            mockEmployeeService
                .Setup(s => s.DeleteHardAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid employeeId) =>
                {
                    return employeeId != unableDeleteEmployeeId;
                });

            return mockEmployeeService;
        }
    }
}
