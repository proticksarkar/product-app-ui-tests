using System;
using System.Text.RegularExpressions;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using WebUIAutomationTestFramework.Driver;
using WebUIAutomationTestFramework.Extensions;
using WebUIAutomationTestFramework.Params;

namespace WebUIAutomationTestFramework.Helpers
{
    public interface IExtentReportsBDDHelper
    {
        void CreateTest();
        void UpdateTestExecutionStatus();
    }

    public class ExtentReportsBDDHelper : IExtentReportsBDDHelper
    {
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _reporter;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly IWebDriver _driver;
        private ExtentTest _scenario;

        public ExtentReportsBDDHelper(ScenarioContext scenarioContext, FeatureContext featureContext, IDriverFixture driverFixture)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _driver = driverFixture.Driver;
        }

        public static void InitializeExtentReports()
        {
            var extentReportPath = DefaultProperties.ExtentReportsLocation;
            _reporter = new ExtentHtmlReporter(extentReportPath);

            if (_extentReports != null)
            {
                return;
            }

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_reporter);
        }

        public void CreateTest()
        {
            var feature = _extentReports.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        public void UpdateTestExecutionStatus()
        {
            var fileName =
                $"{_featureContext.FeatureInfo.Title.Trim()}_{Regex.Replace(_scenarioContext.ScenarioInfo.Title, @"\s", "")}";

            if (_scenarioContext.TestError == null)
                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.When:
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            else
                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case StepDefinitionType.Given:
                        _scenario
                            .CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, _driver.CaptureScreenshotAndReturnModel(fileName));
                        break;
                    case StepDefinitionType.When:
                        _scenario
                            .CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, _driver.CaptureScreenshotAndReturnModel(fileName));
                        break;
                    case StepDefinitionType.Then:
                        _scenario
                            .CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, _driver.CaptureScreenshotAndReturnModel(fileName));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        public static void TearDownReport()
        {
            _extentReports.Flush();
        }
    }
}
