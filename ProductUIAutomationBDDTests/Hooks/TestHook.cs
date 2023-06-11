using WebUIAutomationTestFramework.Helpers;

namespace ProductUIAutomationBDDTests.Hooks
{
    [Binding]
    public class TestHook
    {
        private readonly IExtentReportsBDDHelper _extentReportsBDDHelper;
        
        public TestHook(IExtentReportsBDDHelper extentReportsBDDHelper)
        {
            _extentReportsBDDHelper = extentReportsBDDHelper;

        }

        [BeforeTestRun]
        public static void BeforeAllTests()
        {
            ExtentReportsBDDHelper.InitializeExtentReports();
        }

        [BeforeScenario]
        public void BeforeEachScenario()
        {
            _extentReportsBDDHelper.CreateTest();
        }

        [AfterStep]
        public void AfterEachScenario()
        {
            _extentReportsBDDHelper.UpdateTestExecutionStatus();
        }

        [AfterTestRun]
        public static void AfterAllTests()
        {
            ExtentReportsBDDHelper.TearDownReport();
        }
    }
}
