using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using WebUIAutomationTestFramework.Driver;
using WebUIAutomationTestFramework.Extensions;
using WebUIAutomationTestFramework.Params;

namespace WebUIAutomationTestFramework.Helpers
{
    public class ExtentReportsHelper
    {
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _extentHtmlReporter;
        private ExtentTest _extentTest;
        private readonly ILogger _logger;
        private readonly IWebDriver _driver;
        private string _failedTestCaseName;

        public ExtentReportsHelper(IDriverFixture driverFixture, ILogger logger)
        {
            _driver = driverFixture.Driver;
            _logger = logger;
        }

        public void InitializeExtentReports()
        {
            _extentHtmlReporter = new ExtentHtmlReporter(DefaultProperties.ExtentReportsLocation);

            if (_extentReports != null)
            {
                return;
            }

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_extentHtmlReporter);
        }

        public void CreateTest(string testName)
        {
            _extentTest = _extentReports.CreateTest(testName);
        }

        public void SetStepStatusInfo(string stepDescription)
        {
            _extentTest.Log(Status.Info, stepDescription);
        }

        public void SetStepStatusPass(string stepDescription)
        {
            _extentTest.Log(Status.Pass, stepDescription);
        }

        public void SetStepStatusPassWithScreenshot(string stepDescription)
        {
            var mediaEntity = _driver.CaptureScreenshotAndReturnModel(
                                TestContext.CurrentContext.Test.Name + "_StepPassed");
            _extentTest.Log(Status.Pass, stepDescription, mediaEntity);
        }

        public void SetStepStatusFail(string stepDescription)
        {
            _extentTest.Log(Status.Fail, stepDescription);
        }

        public void SetStepStatusSkip(string stepDescription)
        {
            _extentTest.Log(Status.Skip, stepDescription);
        }

        public void SetStepStatusWarning(string stepDescription)
        {
            _extentTest.Log(Status.Warning, stepDescription);
        }

        public void SetTestStatusPass(string message)
        {
            _extentTest.Pass(message);
        }

        public void SetTestStatusFail(string message)
        {
            var printMessage = "<p><b>Test FAILED!</b></p>";
            if (!string.IsNullOrEmpty(message))
            {
                printMessage += $"Message: <br>{message}<br>";
            }
            var mediaEntity = _driver.CaptureScreenshotAndReturnModel(
                                TestContext.CurrentContext.Test.Name + "_Failed");
            var testName = TestContext.CurrentContext.Test.Name;
            _failedTestCaseName = testName.Substring(0, testName.IndexOf("("));
            _extentTest.Fail(printMessage, mediaEntity);
        }

        public void SetTestStatusWarning()
        {
            _extentTest.Warning("Warning!");
        }

        public void SetTestStatusSkip()
        {
            _extentTest.Skip("Test skipped!");
        }

        public void SetTestStatusSkipDueToPreviousError()
        {
            _extentTest.Skip("Test skipped due to failure of prior test case: " + _failedTestCaseName);
        }

        public void UpdateTestStatus()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";

            if (testStatus == TestStatus.Passed)
            {
                SetTestStatusPass("Test execution successful!");
                _logger.Info($"Test Execution Status: {testStatus}");
            }
            else if (testStatus == TestStatus.Failed)
            {
                SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stackTrace}<br>");
                _logger.Error($"<br>{errorMessage}<br>Stack Trace: <br>{stackTrace}<br>");
                _logger.Error($"Test Execution Status: {testStatus}");
                //IsTestFailed = true;
            }
            else if (testStatus == TestStatus.Skipped)
            {
                //if (IsTestFailed)
                //{
                //    _extentReportsHelper.SetTestStatusSkipForPreviousError();
                //}
                //else
                //{
                //    _extentReportsHelper.SetTestStatusSkip();
                //}
                _logger.Warn("Test Execution Status: Skipped");
            }
            else
            {
                SetTestStatusWarning();
                _logger.Warn($"Test Execution Status: {testStatus}");
            }
        }

        public void TearDownReport()
        {
            _extentReports.Flush();
        }
    }
}
