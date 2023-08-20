using eShopOnlineTestingUtilities.FakeDataGenerators;

namespace eShopOnlineTestingUtilities
{
    public sealed class FakeDataManager
    {
        private readonly Lazy<CompanyGenerator> _companyGenerator;
        private readonly Lazy<EmployeeGenerator> _employeeGenerator;

        public FakeDataManager() 
        {
            _companyGenerator = new Lazy<CompanyGenerator>(() => new CompanyGenerator());
            _employeeGenerator = new Lazy<EmployeeGenerator>(() => new EmployeeGenerator());
        }

        public CompanyGenerator Company => _companyGenerator.Value;

        public EmployeeGenerator Employee => _employeeGenerator.Value;

    }
}
