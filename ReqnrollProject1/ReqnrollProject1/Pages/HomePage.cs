using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.Collections.Specialized.BitVector32;
namespace MarsAutomation.Pages;


public class HomePage
{

    private readonly IWebDriver driver;

    public HomePage(IWebDriver driver)
    {
        this.driver = driver;
    }
    public bool IsLoggedIn()
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
   
    public void NavigateToLanguages()
     {
         IWebElement languagesTab =
             driver.FindElement(
                 By.XPath("//a[normalize-space()='Languages']"));
        //
        languagesTab.Click();
        Thread.Sleep(5000);

     }
   
    public void NavigateToSkills()
    {
        IWebElement SkillsTab =
            driver.FindElement(
                By.XPath("//a[normalize-space()='Skills']"));
        
        SkillsTab.Click();
        Thread.Sleep(5000);
    }

}
