using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace WebUIAutomationTestFramework.Extensions
{
    public static class WaitConditions
    {
        public static Func<IWebDriver, bool> WebElementIsDisplayed(IWebElement element)
        {
            bool condition(IWebDriver driver)
            {
                return element.Displayed;
            }
            return condition;
        }

        public static Func<IWebDriver, IWebElement> WebElementIfDisplayed(IWebElement element)
        {
            IWebElement condition(IWebDriver driver)
            {
                try
                {
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (ElementNotVisibleException)
                {
                    return null;
                }
            }
            return condition;
        }

        public static Func<IWebDriver, IWebElement> WebElementIsClickable(IWebElement element)
        {
            var condition = ExpectedConditions.ElementToBeClickable(element);
            return condition;
        }

        public static Func<IWebDriver, bool> WebElementIsSelectable(IWebElement element)
        {
            var condition = ExpectedConditions.ElementToBeSelected(element);
            return condition;
        }

        public static Func<IWebDriver, bool> WebElementIsStale(IWebElement element)
        {
            var condition = ExpectedConditions.StalenessOf(element);
            return condition;
        }

        public static Func<IWebDriver, bool> WebElementIsInvisible(By by)
        {
            var condition = ExpectedConditions.InvisibilityOfElementLocated(by);
            return condition;
        }

        public static Func<IWebDriver, IAlert> AlertIfPresent()
        {
            var condition = ExpectedConditions.AlertIsPresent();
            return condition;
        }

        public static Func<IWebDriver, bool> TitleIsVerified(string title)
        {
            var condition = ExpectedConditions.TitleIs(title);
            return condition;
        }

        public static Func<IWebDriver, bool> TextIsVerifiedInWebElement(IWebElement element, string text)
        {
            var condition = ExpectedConditions.TextToBePresentInElement(element, text);
            return condition;
        }
    }
}
