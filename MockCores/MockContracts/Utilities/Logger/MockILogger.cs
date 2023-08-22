using Microsoft.Extensions.Logging;

namespace MockContracts.Utilities.Logger
{
    public sealed class MockILogger<TController>
    {
        public static Mock<ILogger<TController>> GetInstance()
        {
            return new Mock<ILogger<TController>>();
        }
    }
}
