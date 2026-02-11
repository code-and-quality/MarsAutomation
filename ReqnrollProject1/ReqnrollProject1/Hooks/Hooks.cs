using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using MarsAutomations.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Hooks
{
    [Binding]
    public class Hooks : CommonDriver
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Clean all languages before ANY test starts
            CommonDriver.driver = new ChromeDriver();
            LoginPage login = new LoginPage(driver);
            login.LoginActions();

            //LanguagePage languagePageObj = new LanguagePage();
            //languagePageObj.DeleteAllLanguages();

            CommonDriver.driver.Quit();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            SetUpSteps();   
            // Opens browser, logs in, navigates to Languages
        }

        [AfterScenario]
        public void AfterScenario()
        {

            // LANGUAGES CLEANUP
            LanguagePage languagePageObj = new LanguagePage(driver);
            foreach (var language in TestDataManager.LanguagesAdded)
            {
                languagePageObj.DeleteLanguage(language);
            }

            // SKILLS CLEANUP
            SkillsPage skillsPageObj = new SkillsPage(driver);
            foreach (var skill in TestDataManager.SkillsAdded)
            {
                skillsPageObj.DeleteSkill(skill);
            }

            // Clear test data for next scenario
            TestDataManager.Clear();

            // Quit browser AFTER cleanup
            CloseDriver();

           
        }
    }
}

