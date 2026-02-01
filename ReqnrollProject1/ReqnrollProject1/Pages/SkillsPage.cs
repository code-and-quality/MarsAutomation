using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomations.Pages
{
    public class SkillsPage
    {
        // Naviagate the Language Tab
        private readonly IWebDriver driver;

        public SkillsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void GoToSkillsTab()
        {
            IWebElement SkillsTab =
            driver.FindElement(
                By.XPath("//a[@data-tab='second']"));
            SkillsTab.Click();
            Wait.WaitToBeClickable(driver, "XPath", "//a[@data-tab='second']", 4);
        }

        //Create a Skill Record
        public void CreateSkillsRecord(IWebDriver driver)
        {


            // Naviagate to Skills Tab

            try
            {
                GoToSkillsTab();


                //naviagate the Add New button

                IWebElement addButtontab = driver.FindElement(By.XPath("//table[.//th[text()='Skill']]//div[contains(@class,'ui teal button')]"));

                //Click on the Add New
                addButtontab.Click();
            }
            catch
            {
                Assert.Fail(" Add button has not been found");
            }

            //naviagte and Type Language into the Add language
            IWebElement addSkillTextbox = driver.FindElement(By.XPath("//input[@placeholder='Add Skill']"));

            addSkillTextbox.SendKeys("JAVA");

            //Select "Choose languages level" from the drop down

            IWebElement SkillLevelDropdown = driver.FindElement(By.XPath("//select[@name='level']"));
            //Create a SelectElement object
            SelectElement selectLevel = new SelectElement(SkillLevelDropdown);

            // Select by visible text (e.g., "Beginner", "Intermediate", "Expert")
            selectLevel.SelectByText("Expert");


            //languageLevelDropdown.Click();


            //Navigate the Add button and Click
            IWebElement addTab = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/span/input[1]"));
            addTab.Click();
            Thread.Sleep(3000);

            IWebElement createdSkill = driver.FindElement(By.XPath("//tr[td[text()='JAVA']]/td[1]"));

            Assert.That(createdSkill.Text == "JAVA", "New Language record has not been created");

            
        }
        public string GetSkill(IWebDriver driver)
        {
            IWebElement newSkill = driver.FindElement(By.XPath("((//th[normalize-space()='Skill']\r\n " +
                   "     /ancestor::table[1]\r\n    " +
                   "  //tbody/tr[td[normalize-space()]]\r\n)[last()]/td[1])"));
            return newSkill.Text;
        }
        public string GetLevel(IWebDriver driver)
        {
            IWebElement newLevel = driver.FindElement(By.XPath("((//th[normalize-space()='Skill']\r\n    " +
                 "  /ancestor::table[1]\r\n    " +
                 "  //tbody/tr[td[normalize-space()]]\r\n)[last()]/td[2])"));
            return newLevel.Text;

        }



        public void EditSkillRecord(IWebDriver driver, string existingSkill, string newSkill, string newLevel)
        {
            try
            { //Naviagate to LanguageTab
                GoToSkillsTab();
               

                // Click Edit button (adjust selector as needed)
                IWebElement editButton = driver.FindElement(By.XPath(
               $"//th[normalize-space()='Skill']/ancestor::table[1]" +
               $"//tbody/tr[td[1][normalize-space()='{existingSkill}']]//i[contains(@class,'write')]"
           ));
                editButton.Click();
            }

            catch (Exception ex)
            {
                Assert.Fail("Edit button has been not located");

            }

            // Update Skill
            IWebElement languageTextbox = driver.FindElement(By.Name("name"));
            languageTextbox.Clear();
            languageTextbox.SendKeys(newSkill);

            // Update level
            IWebElement levelDropdown = driver.FindElement(By.Name("level"));
            levelDropdown.Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{newLevel}']")).Click();

            // Save
            driver.FindElement(By.XPath("//input[@value='Update']")).Click();

           
            Thread.Sleep(5000);

            
        }

        

        public string GetUpdatedSkill(IWebDriver driver, string expectedSkill)
        {
            IWebElement updatedSkill = driver.FindElement(
                By.XPath($"//table[.//th[normalize-space()='Skill']]//td[normalize-space()='{expectedSkill}']")
            );

            return updatedSkill.Text;
        }
        public String GetUpdatedSkillsLevel(IWebDriver driver, string expectedLevel)
        {
            IWebElement updatedLevel = driver.FindElement(By.XPath($"//table[.//th[normalize-space()='Skill']]//td[normalize-space()='{expectedLevel}']"));
            return updatedLevel.Text;

        }
        public void DeleteSkillRecord(IWebDriver driver,string skillToDelete)
        {
            try
            {
                GoToSkillsTab();

                // Click delete icon for the row matching the language
                IWebElement deleteButton = driver.FindElement(By.XPath(
                     $"//table[.//th[normalize-space()='Skill']]//tr[td[1][normalize-space()='{skillToDelete}']]//i[contains(@class,'remove')]"));
                deleteButton.Click();
                Thread.Sleep(3000);

            }
            catch (Exception ex)

            {
                Assert.Fail("Delete button has been not located");
            }



        }
        public bool IsSkillDeleted(IWebDriver driver, string skill)
        {
            var rows = driver.FindElements(By.XPath(
                $"//th[normalize-space()='Skill']/ancestor::table[1]" +
                $"//tbody/tr[td[1][normalize-space()='{skill}']]"
            ));

            return rows.Count == 0;
        }

        public void AddDuplicateSkill(IWebDriver driver, string skill, string level)
        {

            // Navigate to Skills tab
            GoToSkillsTab();

            // Click Add New
            driver.FindElement(By.XPath("//table[.//th[text()='Skill']]//div[contains(@class,'ui teal button')]")).Click();

            // Enter skill
            IWebElement skillTextbox = driver.FindElement(By.Name("name"));
            skillTextbox.Clear();
            skillTextbox.SendKeys(skill);

            // Select level
            IWebElement levelDropdown = driver.FindElement(By.Name("level"));
            levelDropdown.Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();
           
            // Click Add
            driver.FindElement(By.XPath("//input[@value='Add']")).Click();

        }

      



        public string GetDuplicateErrorMessage(IWebDriver driver)
        {
            IWebElement message = driver.FindElement(By.XPath("//div[contains(@class,'ns-box') and contains(@class,'ns-show')]//div[@class='ns-box-inner']"));
            return message.Text;
        }
    }
}

