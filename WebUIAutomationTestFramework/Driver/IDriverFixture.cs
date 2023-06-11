using System.Collections.Generic;
using OpenQA.Selenium;

namespace WebUIAutomationTestFramework.Driver;
public interface IDriverFixture
{
    IWebDriver Driver { get; }
}
