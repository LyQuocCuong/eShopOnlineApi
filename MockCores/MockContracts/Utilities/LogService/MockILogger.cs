using Microsoft.Extensions.Logging;

namespace MockContracts.Utilities.LogService
{
    public sealed class MockILogger<TController>
    {
        public static Mock<ILogger<TController>> GetInstance()
        {
            return new Mock<ILogger<TController>>(MockBehavior.Loose);  // just for Initializing Constructor
        }
    }
}
