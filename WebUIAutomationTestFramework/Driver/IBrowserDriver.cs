using OpenQA.Selenium;

namespace WebUIAutomationTestFramework.Driver;
public interface IBrowserDriver
{
    IWebDriver GetChromeDriver();
    IWebDriver GetFirefoxDriver();
    IWebDriver GetEdgeDriver();
    IWebDriver GetRemoteWebDriver();
}
