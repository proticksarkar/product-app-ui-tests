using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebUIAutomationTestFramework.Settings;

namespace WebUIAutomationTestFramework.Driver;

public class BrowserDriver : IBrowserDriver
{
    private readonly TestSettings _testSettings;

    public BrowserDriver(TestSettings testSettings)
    {
        _testSettings = testSettings;
    }

    public IWebDriver GetChromeDriver()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        return new ChromeDriver(GetBrowserOptions());
    }

    public IWebDriver GetFirefoxDriver()
    {
        new DriverManager().SetUpDriver(new FirefoxConfig());
        return new FirefoxDriver(GetBrowserOptions());
    }

    public IWebDriver GetEdgeDriver()
    {
        new DriverManager().SetUpDriver(new EdgeConfig());
        return new EdgeDriver(GetBrowserOptions());
    }

    public IWebDriver GetRemoteWebDriver()
    {
        return new RemoteWebDriver(_testSettings.SeleniumGridUrl, GetBrowserOptions());
    }

    private dynamic GetBrowserOptions()
    {
        switch (_testSettings.BrowserType)
        {
            case BrowserType.Chrome:
                {
                    var chromeOption = new ChromeOptions();
                    chromeOption.AddAdditionalOption("se:recordVideo", true);
                    chromeOption.AddArgument("--start-maximized");
                    return chromeOption;
                }
            case BrowserType.Firefox:
                {
                    var firefoxOption = new FirefoxOptions();
                    firefoxOption.AddAdditionalOption("se:recordVideo", true);
                    firefoxOption.AddArgument("--start-maximized");
                    return firefoxOption;
                }
            case BrowserType.Edge:
                {
                    var edgeOption = new EdgeOptions();
                    edgeOption.AddAdditionalOption("se:recordVideo", true);
                    edgeOption.AddArgument("--start-maximized");
                    return edgeOption;
                }
            default:
                return new ChromeOptions();
        }
    }
}

public enum BrowserType
{
    Chrome,
    Firefox,
    Edge
}
