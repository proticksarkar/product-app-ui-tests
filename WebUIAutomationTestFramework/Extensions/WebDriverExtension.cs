using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebUIAutomationTestFramework.Settings;
using OpenQA.Selenium.Support.Extensions;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using AventStack.ExtentReports;

namespace WebUIAutomationTestFramework.Extensions
{
    public static class WebDriverExtension
    {
        // Screenshot utility
        public static string CaptureScreenshotAndReturnPath(this IWebDriver driver, string fileName)
        {
            var screenshot = driver.TakeScreenshot();
            var screenshotPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}//{fileName}.png";
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        public static MediaEntityModelProvider CaptureScreenshotAndReturnModel(this IWebDriver driver, string fileName)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, fileName).Build();
        }

        // Search extensions
        public static IWebElement FindWebElement(this IWebDriver driver, By by)
        {
            var webElement = WaitUntil(driver, drv => drv.FindElement(by));
            return webElement;
        }

        public static IEnumerable<IWebElement> FindWebElements(this IWebDriver driver, By by)
        {
            var webElements = WaitUntil(driver, drv => drv.FindElements(by));
            return webElements;
        }

        // Alert
        public static void AcceptAlert(this IWebDriver driver)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public static void DismissAlert(this IWebDriver driver)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
        }

        public static string GetTextFromAlert(this IWebDriver driver)
        {
            IAlert alert = driver.SwitchTo().Alert();
            return alert.Text;
        }

        public static void PerformAlertAuthentication(this IWebDriver driver, string userName, string password)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.SetAuthenticationCredentials(userName, password);
            alert.Accept();
        }

        // Actions
        public static void Hover(this IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        // Window
        public static void MaximizeWindow(this IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

        public static void SwitchToNewWindow(this IWebDriver driver)
        {
            driver.SwitchTo().NewWindow(WindowType.Window);
        }

        public static void SwitchToNewTab(this IWebDriver driver)
        {
            driver.SwitchTo().NewWindow(WindowType.Tab);
        }

        public static ReadOnlyCollection<string> GetCurrentWindows(this IWebDriver driver)
        {
            return driver.WindowHandles;
        } 

        public static void SwitchToWindow(this IWebDriver driver, int windowIndex)
        {
            var currentWindows = GetCurrentWindows(driver);
            driver.SwitchTo().Window(currentWindows[windowIndex]);
        }

        // Frame
        public static void SwitchToFrame(this IWebDriver driver, int frameIndex)
        {
            driver.SwitchTo().Frame(frameIndex);
        }

        public static void SwitchToFrame(this IWebDriver driver, string frameName)
        {
            driver.SwitchTo().Frame(frameName);
        }

        public static void SwitchToParentFrame(this IWebDriver driver)
        {
            driver.SwitchTo().ParentFrame();
        }

        // Page
        public static void ReloadPage(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        public static void NavigateForward(this IWebDriver driver)
        {
            driver.Navigate().Forward();
        }

        public static void NavigateBackward(this IWebDriver driver)
        {
            driver.Navigate().Back();
        }

        // JavaScriptExecutor
        public static void ScrollElementInView(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", element);
        }

        public static void ScrollToPageTop(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");
        }

        public static void ScrollToPageBottom(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        // WebDriverWait extensions
        public static bool WaitUntil(this IWebDriver driver, Func<IWebDriver, bool> condition)
        {
            TestSettings testSettings = new TestSettings();
            var wait = GetDriverWait(driver, testSettings.TimeoutInterval, testSettings.PollingInterval);
            return wait.Until(condition);
        }

        public static IWebElement WaitUntil(this IWebDriver driver, Func<IWebDriver, IWebElement> condition)
        {
            TestSettings testSettings = new TestSettings();
            var wait = GetDriverWait(driver, testSettings.TimeoutInterval, testSettings.PollingInterval);
            return wait.Until(condition);
        }

        public static IEnumerable<IWebElement> WaitUntil(this IWebDriver driver, Func<IWebDriver, IEnumerable<IWebElement>> condition)
        {
            TestSettings testSettings = new TestSettings();
            var wait = GetDriverWait(driver, testSettings.TimeoutInterval, testSettings.PollingInterval);
            return wait.Until(condition);
        }

        // WebDriverWait
        private static WebDriverWait GetDriverWait(this IWebDriver driver, int timeoutInterval, int pollingInterval)
        {
            var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(timeoutInterval))
            {
                PollingInterval = TimeSpan.FromSeconds(pollingInterval)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait;
        }
    }
}
