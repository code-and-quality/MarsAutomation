using MarsAutomation.Utilities;
using MarsAutomation.Pages;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace MarsAutomation.Tests
{
    [TestFixture]
    public class LanguagesTests : CommonDriver
    {
        [SetUp]
        public void SetUpSteps() 
        
        {
            driver = new ChromeDriver();
            //Login Page object initialisation and definition
            LoginPage LoginPageObj = new LoginPage();
            LoginPageObj.LoginActions(driver);
            //Homepage Object Initialisation and definition

            HomePage HomePageObj = new HomePage();
            HomePageObj.NavigateToLanguages(driver);

        }

        [Test]
        public void CreateLanguageTest()

        {
            LanguagePage LanguagePageObj = new LanguagePage(driver);
           // LanguagePageObj.CreateLanguageRecord(driver);

        }
        [Test]
        public void EditLanguageTest()

        {
            LanguagePage LanguagePageObj = new LanguagePage(driver);
           // LanguagePageObj.EditLanguageRecord(driver);
        }
        [Test]
        public void DeleteLanguageTest()

        {
            LanguagePage LanguagePageObj = new LanguagePage(driver);
            //LanguagePageObj.DeleteLanguageRecord(driver);
        }
        public void CloseTestRun()

        {

        }
    }
}
