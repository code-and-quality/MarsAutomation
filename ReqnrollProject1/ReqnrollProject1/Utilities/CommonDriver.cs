using MarsAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MarsAutomation.Utilities
{
    public class CommonDriver
    {
        public static IWebDriver driver ;
        public void SetUpSteps()
        {
            driver = new ChromeDriver();

            // Login
            LoginPage loginPageObj = new LoginPage(driver);
            loginPageObj.LoginActions();

         }

        public void CloseDriver()
        {
            driver.Quit();
        }

    }
}
