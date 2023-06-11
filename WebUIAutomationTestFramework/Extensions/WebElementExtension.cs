using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebUIAutomationTestFramework.Extensions;
public static class WebElementExtension
{
    // WebElement
    public static void ClearAndEnterText(this IWebElement element, string value)
    {
        element.Clear();
        element.SendKeys(value);
    }

    public static void SelectDropDownByIndex(this IWebElement element, int index)
    {
        var select = new SelectElement(element);
        select.SelectByIndex(index);
    }

    public static void SelectDropDownByText(this IWebElement element, string text)
    {
        var select = new SelectElement(element);
        select.SelectByText(text);
    }

    public static void SelectDropDownByValue(this IWebElement element, string value)
    {
        var select = new SelectElement(element);
        select.SelectByValue(value);
    }

    public static string GetSelectedDropDownValue(this IWebElement element)
    {
        var select = new SelectElement(element);
        return select.SelectedOption.Text;
    }

    // List of WebElement
    public static IList<IWebElement> GetAllSelectedOptions(this IWebElement element)
    {
        var select = new SelectElement(element);
        return select.AllSelectedOptions;
    }

    public static void ClickEachWebElement(this IEnumerable<IWebElement> elements)
    {
        foreach (var element in elements)
        {
            element.Click();
        }
    }

    public static IList<string> GetTextFromEachWebElement(this IEnumerable<IWebElement> elements)
    {
        IList<string> elementTextList = new List<string>();
        foreach (var element in elements)
        {
            elementTextList.Add(element.Text);
        }
        return elementTextList;
    }
}
