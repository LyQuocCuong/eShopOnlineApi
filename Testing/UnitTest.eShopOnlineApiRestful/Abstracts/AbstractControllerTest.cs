using eShopOnlineTestingUtilities;

namespace UnitTest.eShopOnlineApiRestful.Abstracts
{
    public abstract class AbstractControllerTest<TControllerTest>
    {
        protected readonly FakeDataManager _fakeDataManager;

        protected readonly Mock<IServiceManager> _stubServices;
        protected readonly Mock<ILogger<TControllerTest>> _stubLogger;
        protected readonly Mock<ControllerParams> _stubControllerParams;

        protected AbstractControllerTest() 
        {
            _fakeDataManager = new FakeDataManager();

            // Strict - require Mocking ENOUGH the needed methods
            _stubServices = new Mock<IServiceManager>(MockBehavior.Strict);

            // Controller's constructor need these Stubs
            _stubLogger = new Mock<ILogger<TControllerTest>>(MockBehavior.Loose);
            _stubControllerParams = new Mock<ControllerParams>(MockBehavior.Loose, _stubServices.Object);
        }

        [SetUp]
        public abstract void SetUpBeforeExecutingEachTest();    // Override to Initialize EACH Controller.

        [TearDown]
        public void ClearAfterExecutingEachTest()
        {
            _stubServices.Reset();  // Clear ALL setups of AFTER EACH TestMethod.
            _stubLogger.Reset();
            _stubControllerParams.Reset();
        }

    }
}
