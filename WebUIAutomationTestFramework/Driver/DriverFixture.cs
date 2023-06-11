using OpenQA.Selenium;
using System;
using WebUIAutomationTestFramework.Helpers;
using WebUIAutomationTestFramework.Settings;

namespace WebUIAutomationTestFramework.Driver;

public class DriverFixture : IDriverFixture, IDisposable
{
    private readonly TestSettings _testSettings;
    private readonly IBrowserDriver _browserDriver;
    private readonly ILogger _logger;
    private readonly IWebDriver _driver;

    // DI is happening
    public DriverFixture(TestSettings testSettings, IBrowserDriver browserDriver, ILogger logger)
    {
        _testSettings = testSettings;
        _browserDriver = browserDriver;
        _logger = logger;

        _driver = _testSettings.ExecutionType == ExecutionType.Local ? GetWebDriver() : browserDriver.GetRemoteWebDriver();
        _logger.Info($"{_testSettings.BrowserType} browser is launched.");

        _driver.Navigate().GoToUrl(_testSettings.ApplicationUrl);
        _logger.Info($"Application opened at {_testSettings.ApplicationUrl}.");
    }

    public IWebDriver Driver => _driver;

    private IWebDriver GetWebDriver()
    {
        return _testSettings.BrowserType switch
        {
            BrowserType.Chrome => _browserDriver.GetChromeDriver(),
            BrowserType.Firefox => _browserDriver.GetFirefoxDriver(),
            BrowserType.Edge => _browserDriver.GetEdgeDriver(),
            _ => _browserDriver.GetChromeDriver()
        };
    }

    public void Dispose()
    {
        _driver.Quit();
        _logger.Info($"{_testSettings.BrowserType} browser window is closed.");
    }
}
