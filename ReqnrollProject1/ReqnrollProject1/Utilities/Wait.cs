using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAutomation.Utilities;

public class Wait
{
    //generic function wait for an element to be clickable

    public static void WaitToBeClickable(IWebDriver driver, string locatorType, string locatorValue, int seconds)
    {

        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email")));
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));
        if (locatorType == "XPath")
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(locatorValue)));
        }

        if (locatorType == "Id")
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(locatorValue)));
        }
    }

    public static void WaitToBeVisible(IWebDriver driver, string locatorType, string locatorValue, int seconds)
    {
        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email")));
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));
        if (locatorType == "XPath")
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locatorValue)));
        }

        if (locatorType == "Id")
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(locatorValue)));
        }
    }
}