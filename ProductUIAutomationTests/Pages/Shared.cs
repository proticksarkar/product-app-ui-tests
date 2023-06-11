using OpenQA.Selenium;
using WebUIAutomationTestFramework.Driver;
using WebUIAutomationTestFramework.Extensions;

namespace ProductUIAutomationTests.Pages;

public interface IShared
{
    void NavigateToHomePage();
    void NavigateToPrivacyPage();
    void NavigateToProductPage();
}

public class Shared : IShared
{
    private readonly IWebDriver _driver;

    public Shared(IDriverFixture driverFixture) => _driver = driverFixture.Driver;

    private IWebElement lnkHome => _driver.FindWebElement(By.LinkText("Home"));
    private IWebElement lnkPrivacy => _driver.FindWebElement(By.LinkText("Privacy"));
    private IWebElement lnkProduct => _driver.FindWebElement(By.LinkText("Product"));

    public void NavigateToHomePage()
    {
        lnkHome.Click();
    }

    public void NavigateToPrivacyPage()
    {
        lnkPrivacy.Click();
    }
    public void NavigateToProductPage()
    {
        lnkProduct.Click();
    }
}
