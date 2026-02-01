using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.Collections.Specialized.BitVector32;
namespace MarsAutomation.Pages;


public class HomePage
{
    public bool IsLoggedIn(IWebDriver driver)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        try
        {
            IWebElement greeting = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[contains(@class,'item ui dropdown link') and contains(text(),'Hi')]")
                )
            );
            return greeting.Displayed;
        }
        catch
        {
            return false;
        }
    }
   
    public void NavigateToLanguages(IWebDriver driver)
     {
         IWebElement languagesTab =
             driver.FindElement(
                 By.XPath("//a[normalize-space()='Languages']"));
        //
        languagesTab.Click();
        Thread.Sleep(5000);

     }
   
    public void NavigateToSkills(IWebDriver driver)
    {
        IWebElement SkillsTab =
            driver.FindElement(
                By.XPath("//a[normalize-space()='Skills']"));
        
        SkillsTab.Click();
        Thread.Sleep(5000);
    }

}
