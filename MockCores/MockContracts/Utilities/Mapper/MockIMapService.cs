using Contracts.Utilities.Mapper;

namespace MockContracts.Utilities.Mapper
{
    public sealed class MockIMapService
    {
        public static Mock<IMapService> GetInstance()
        {
            return new Mock<IMapService>();
        }
    }
}
