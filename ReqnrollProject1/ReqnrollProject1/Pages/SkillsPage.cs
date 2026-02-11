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
        //Add Skill
        public void AddSkill(string skill, string level)
        {
            // Find Add New button
            var addNewButtons = driver.FindElements(By.XPath("//table[.//th[text()='Skill']]//div[contains(@class,'ui teal button')]"));

            // If button does not exist OR is not visible OR is disabled → stop
            if (addNewButtons.Count == 0 || !addNewButtons[0].Displayed || !addNewButtons[0].Enabled)
                return;

            // Wait until clickable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addNewButtons[0]));

            // Click Add New
            addNewButtons[0].Click();

            // Fill language
            driver.FindElement(By.Name("name")).SendKeys(skill);

            // Select level
            driver.FindElement(By.Name("level")).Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();

            // Click Add
            driver.FindElement(By.XPath("//input[@value='Add']")).Click();
        }

        //Create a Skill Record
        public void CreateSkillsRecord()
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

        public string GetSkill(string expectedSkill)
        {
            try
            {
                string xpath = $"//table/tbody/tr/td[normalize-space()='{expectedSkill}']";
                return driver.FindElement(By.XPath(xpath)).Text.Trim();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Skill '{expectedSkill}' not found in table.");
                return string.Empty;
            }
        }

        public String GetLevel(string newLevel)
        {
            IWebElement createdLevel = driver.FindElement(By.XPath($"//table[.//th[normalize-space()='Skill']]//td[normalize-space()='{newLevel}']"));
            return createdLevel.Text;

        }
   

        public void EditSkillRecord(string existingSkill, string newSkill, string newLevel)
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

        

        public string GetUpdatedSkill(string expectedSkill)
        {
            IWebElement updatedSkill = driver.FindElement(
                By.XPath($"//table[.//th[normalize-space()='Skill']]//td[normalize-space()='{expectedSkill}']")
            );

            return updatedSkill.Text;
        }
        public String GetUpdatedSkillsLevel(string expectedLevel)
        {
            IWebElement updatedLevel = driver.FindElement(By.XPath($"//table[.//th[normalize-space()='Skill']]//td[normalize-space()='{expectedLevel}']"));
            return updatedLevel.Text;

        }
        public void DeleteSkill(string skill)
        {
            try
            {
                string deleteXPath =
                    $"//table/tbody/tr[td[1][normalize-space()='{skill}']]/td[3]/span[2]/i";

                IWebElement deleteButton = driver.FindElement(By.XPath(deleteXPath));
                deleteButton.Click();

                Thread.Sleep(500); // allow UI to refresh
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Skill '{skill}' not found for deletion.");
            }
        }

         
      
        public void DeleteSkillRecord(string skillToDelete)
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
        public bool IsSkillDeleted(string skill)
        {
            var rows = driver.FindElements(By.XPath(
                $"//th[normalize-space()='Skill']/ancestor::table[1]" +
                $"//tbody/tr[td[1][normalize-space()='{skill}']]"
            ));

            return rows.Count == 0;
        }

        public void AddDuplicateSkill(string skill, string level)
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

      
        public string GetDuplicateErrorMessage()
        {
            IWebElement message = driver.FindElement(By.XPath("//div[contains(@class,'ns-box') and contains(@class,'ns-show')]//div[@class='ns-box-inner']"));
            return message.Text;
        }
    }
}

