using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using MarsAutomations.Pages;
using Reqnroll;

namespace MarsAutomation.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly CommonDriver commonDriver;

        public Hooks(CommonDriver commonDriver)
        {
            this.commonDriver = commonDriver;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            commonDriver.InitializeDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = commonDriver.driver;

            // LANGUAGES CLEANUP
            if (TestDataManager.LanguagesAdded.Any())
            {
                var languagePage = new LanguagePage(driver);
                languagePage.GoToLanguagesTab();

                foreach (var language in TestDataManager.LanguagesAdded)
                {
                    if (!languagePage.IsLanguageDeleted(language))
                    {
                        languagePage.DeleteLanguage(language);
                    }
                }
                
            }

            // SKILLS CLEANUP
            if (TestDataManager.SkillsAdded.Any())
            {
                var skillsPage = new SkillsPage(driver);
                skillsPage.GoToSkillsTab();

                foreach (var skill in TestDataManager.SkillsAdded)
                {
                    if (!skillsPage.IsSkillDeleted(skill))
                    { 
                    skillsPage.DeleteSkill(skill);
                    }
                }
            }

            // Clear test data for next scenario
            TestDataManager.Clear();

            // Quit browser AFTER cleanup
            commonDriver.CloseDriver();
        }
    }
}


