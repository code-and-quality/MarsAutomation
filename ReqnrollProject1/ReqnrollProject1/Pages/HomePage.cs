using MarsAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Collections.Specialized.BitVector32;
namespace MarsAutomation.Pages;


public class HomePage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public HomePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    //Locators

    private By GreetingText =>
        By.XPath("//span[contains(@class,'item ui dropdown link') and contains(text(),'Hi')]");

    private By LanguagesTab =>
        By.XPath("//a[@data-tab='first']");
       // By.XPath("//a[normalize-space()='Languages']");

    private By SkillsTab =>
        By.XPath("//a[normalize-space()='Skills']");

    //Actions

    public bool IsLoggedIn()
    {
        try
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(GreetingText)).Displayed;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public void NavigateToLanguages()
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(LanguagesTab)).Click();
    }

    

    public void NavigateToSkills()
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(SkillsTab)).Click();
    }


}
