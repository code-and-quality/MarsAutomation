using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsAutomations.Pages
{
    public class SkillsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // -----------------------------
        // LOCATORS
        // -----------------------------
        private readonly By skillsTab = By.XPath("//a[@data-tab='second']");
        private readonly By addNewButton = By.XPath("//table[.//th[text()='Skill']]//div[contains(@class,'ui teal button')]");
        private readonly By skillTextbox = By.Name("name");
        private readonly By levelDropdown = By.Name("level");
        private readonly By addButton = By.XPath("//input[@value='Add']");
        private readonly By updateButton = By.XPath("//input[@value='Update']");
        private readonly By notificationMessage = By.XPath("//div[contains(@class,'ns-box') and contains(@class,'ns-show')]//div[@class='ns-box-inner']");

        // Dynamic locators
        private By SkillRow(string skill) =>
            By.XPath($"//tbody/tr[td[1][normalize-space()='{skill}']]");

        private By SkillCell(string skill) =>
            By.XPath($"//table[.//th='Skill']//td[normalize-space()='{skill}']");

        private By LevelCell(string level) =>
            By.XPath($"//table[.//th='Skill']//td[normalize-space()='{level}']");

        private By EditButton(string skill) =>
            By.XPath($"//tbody/tr[td[1][normalize-space()='{skill}']]//i[contains(@class,'write')]");

        private By DeleteButton(string skill) =>
            By.XPath($"//tbody/tr[td[1][normalize-space()='{skill}']]//i[contains(@class,'remove')]");

        private By UpdatedSkillCell(string skill) =>
        By.XPath($"//table[.//th[normalize-space()='Skill']]//td[normalize-space()='{skill}']");

        private By UpdatedLevelCell(string level) =>
            By.XPath($"//table[.//th[normalize-space()='Skill']]//td[normalize-space()='{level}']");

        // -----------------------------
        // CONSTRUCTOR
        // -----------------------------
        public SkillsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        // -----------------------------
        // METHODS
        // -----------------------------

        public void GoToSkillsTab()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(skillsTab)).Click();
        }

        public int GetSkillCount()
        {
            var rows = driver.FindElements(By.XPath("//th[normalize-space()='Skill']/ancestor::table[1]//tbody/tr"));
            return rows.Count;
        }

        public void AddSkill(string skill, string level)
        {
            GoToSkillsTab();

            wait.Until(ExpectedConditions.ElementToBeClickable(addNewButton)).Click();

            var textbox = wait.Until(ExpectedConditions.ElementIsVisible(skillTextbox));
            textbox.Clear();
            textbox.SendKeys(skill);

            wait.Until(ExpectedConditions.ElementToBeClickable(levelDropdown)).Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(addButton)).Click();
        }

        public string GetSkill(string skill)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(SkillCell(skill))).Text;
        }

        public string GetLevel(string level)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(LevelCell(level))).Text;
        }

        public void EditSkill(string existingSkill, string newSkill, string newLevel)
        {
            GoToSkillsTab();

            wait.Until(ExpectedConditions.ElementToBeClickable(EditButton(existingSkill))).Click();

            var textbox = wait.Until(ExpectedConditions.ElementIsVisible(skillTextbox));
            textbox.Clear();
            textbox.SendKeys(newSkill);

            wait.Until(ExpectedConditions.ElementToBeClickable(levelDropdown)).Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{newLevel}']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(updateButton)).Click();
        }
        public void EditSkillRecord(string existingSkill, string newSkill, string newLevel)
        {
            GoToSkillsTab();

            // Click edit icon
            var editBtn = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    EditButton(existingSkill)
                )
            );
            editBtn.Click();

            // Update skill name
            var textbox = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(skillTextbox)
            );
            textbox.Clear();
            textbox.SendKeys(newSkill);

            // Update level
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(levelDropdown)
            ).Click();

            driver.FindElement(By.XPath($"//option[normalize-space()='{newLevel}']")).Click();

            // Save
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(updateButton)
            ).Click();
        }

        public string GetUpdatedSkill(string expectedSkill)
        {
            var element = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    UpdatedSkillCell(expectedSkill)
                )
            );

            return element.Text;
        }

        public string GetUpdatedSkillsLevel(string expectedLevel)
        {
            var element = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    UpdatedLevelCell(expectedLevel)
                )
            );

            return element.Text;
        }
        public void DeleteSkill(string skill)
        {
            GoToSkillsTab();

            var deleteButtons = driver.FindElements(DeleteButton(skill));

            if (deleteButtons.Count == 0)
                return; // Already deleted or not present

            wait.Until(ExpectedConditions.ElementToBeClickable(deleteButtons[0])).Click();
        }

        public bool IsSkillDeleted(string skill)
        {
            return driver.FindElements(SkillRow(skill)).Count == 0;
        }
        public void AddDuplicateSkill(string skill, string level)
        {
            GoToSkillsTab();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addNewButton))
                .Click();

            var textbox = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(skillTextbox)
            );
            textbox.Clear();
            textbox.SendKeys(skill);

            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(levelDropdown)
            ).Click();

            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();

            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addButton)
            ).Click();
        }

        public string GetNotificationMessage()
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(notificationMessage)).Text;
        }
    }
}
