using MarsAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MarsAutomation.Utilities
{
        public class CommonDriver
        {
            public IWebDriver driver;

            public void InitializeDriver()
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
            }

            public void CloseDriver()
            {
                driver.Quit();
            }
        }
    }



