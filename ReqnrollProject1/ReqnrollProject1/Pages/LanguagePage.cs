using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Log;
using OpenQA.Selenium.Support.UI;
using Reqnroll.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Pages
{
    public class LanguagePage
    {
        // Naviagate the Language Tab
        private readonly IWebDriver driver;

        public LanguagePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void GoToLanguagesTab()
        {
            IWebElement languagesTab =
            driver.FindElement(
                By.XPath("//a[@data-tab='first']"));
            languagesTab.Click();
            Wait.WaitToBeClickable(driver, "XPath", "//a[@data-tab='first']", 4);
        }
        public int GetLanguageCount()
        {
            var rows = driver.FindElements(By.XPath("//th[normalize-space()='Language']/ancestor::table[1]//tbody/tr"));
            return rows.Count;
        }

        public void AddLanguage(string language, string level)
        {
            // Find Add New button
            var addNewButtons = driver.FindElements(By.XPath("//table[.//th[text()='Language']]//div[contains(@class,'ui teal button')]"));

            // If button does not exist OR is not visible OR is disabled → stop
            if (addNewButtons.Count == 0 || !addNewButtons[0].Displayed || !addNewButtons[0].Enabled)
                return;

            // Wait until clickable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addNewButtons[0]));

            // Click Add New
            addNewButtons[0].Click();

            // Fill language
            driver.FindElement(By.Name("name")).SendKeys(language);

            // Select level
            driver.FindElement(By.Name("level")).Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();

            // Click Add
            driver.FindElement(By.XPath("//input[@value='Add']")).Click();
        }

        public bool IsAddNewButtonVisible(IWebDriver driver)
        {
            var buttons = driver.FindElements(By.XPath("//table[.//th[text()='Language']]//div[contains(@class,'ui teal button')]"));
            return buttons.Count > 0 && buttons[0].Displayed;
        }

        //Create a Languages Record
        public void CreateLanguageRecord()
        {


            // Naviagate to Languages Tab

            try
            {
                GoToLanguagesTab();


                //naviagate the Add New button

                IWebElement addButtontab = driver.FindElement(By.XPath("//table[.//th[text()='Language']]//div[contains(@class,'ui teal button')]"));

                //Click on the Add New
                addButtontab.Click();
            }
            catch 
            {
            Assert.Fail(" Add button has not been found");
            }

            //naviagte and Type Language into the Add language
            IWebElement addLanguageTextbox = driver.FindElement(By.XPath("//input[@placeholder='Add Language']"));

            addLanguageTextbox.SendKeys("Tulu");

            //Select "Choose languages level" from the drop down

            IWebElement languageLevelDropdown = driver.FindElement(By.XPath("//select[@name='level']"));
             //Create a SelectElement object
            SelectElement selectLevel = new SelectElement(languageLevelDropdown);

            // Select by visible text (e.g., "Fluent", "Basic", "Conversational")
            selectLevel.SelectByText("Fluent");

            //Navigate the Add button and Click
            IWebElement addTab = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[3]/input[1]"));
            addTab.Click();
            Thread.Sleep(5000);


        
        }

        public String GetLanguage(string newLanguage)
        {
            IWebElement createdLanguage = driver.FindElement(By.XPath($"//table[.//th[normalize-space()='Language']]//td[normalize-space()='{newLanguage}']"));
            return createdLanguage.Text;
        }
        public String GetLevel(string newLevel)
        {
            IWebElement createdLevel = driver.FindElement(By.XPath($"//table[.//th[normalize-space()='Language']]//td[normalize-space()='{newLevel}']"));
            return createdLevel.Text;

        }
      
        public void EditLanguageRecord(string existingLanguage, string newLanguage, string newLevel)
        {
            try
            {
                // Click edit icon for the row matching the language
                IWebElement editButton = driver.FindElement(By.XPath(
                    $"//th[normalize-space()='Language']/ancestor::table[1]" +
                    $"//tbody/tr[td[1][normalize-space()='{existingLanguage}']]//i[contains(@class,'write')]"
                ));
                editButton.Click();
            }

            catch (Exception ex)
            {
                Assert.Fail("Edit button has been not located");

            }

            // Update language
            IWebElement languageTextbox = driver.FindElement(By.Name("name"));
            languageTextbox.Clear();
            languageTextbox.SendKeys(newLanguage);

            // Update level
            IWebElement levelDropdown = driver.FindElement(By.Name("level"));
            levelDropdown.Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{newLevel}']")).Click();

            // Save
            driver.FindElement(By.XPath("//input[@value='Update']")).Click();
            Thread.Sleep(5000);
        }
        public String GetUpdatedLanguage(string expectedLanguage)
        {
            IWebElement updatedLanguage = driver.FindElement(By.XPath($"//table[.//th[normalize-space()='Language']]//td[normalize-space()='{expectedLanguage}']"));
            return updatedLanguage.Text;
        }
        public String GetUpdatedLanguageLevel(IWebDriver driver,string expectedLevel)
        {
            IWebElement updatedLevel = driver.FindElement(By.XPath($"//table[.//th[normalize-space()='Language']]//td[normalize-space()='{expectedLevel}']"));
            return updatedLevel.Text;

        }

        public void DeleteLanguage(string language)
        {
            try
            {
                string deleteXPath =
                    $"//table/tbody/tr[td[1][normalize-space()='{language}']]/td[3]/span[2]/i";

                IWebElement deleteButton = driver.FindElement(By.XPath(deleteXPath));
                deleteButton.Click();

                Thread.Sleep(500); // allow UI to refresh
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Language '{language}' not found for deletion.");
            }
        }


        public void DeleteLanguageRecord(string languageToDelete)
        {
            try
            { // Navigate to Languages tab
                GoToLanguagesTab();

                // Click delete icon for the row matching the language
                IWebElement deleteButton = driver.FindElement(By.XPath(
                    $"//th[normalize-space()='Language']/ancestor::table[1]" +
                    $"//tbody/tr[td[1][normalize-space()='{languageToDelete}']]//i[contains(@class,'remove')]"
                ));
                deleteButton.Click();
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Delete button has been not located");
            }


            }
        public bool IsLanguageDeleted(string language)
        {
            var rows = driver.FindElements(By.XPath(
                $"//th[normalize-space()='Language']/ancestor::table[1]" +
                $"//tbody/tr[td[1][normalize-space()='{language}']]"
            ));

            return rows.Count == 0;
        }

        public void AddDuplicateLanguage(string language, string level)
        {

            // Navigate to Languages tab
            GoToLanguagesTab();
            
            // Click Add New
            driver.FindElement(By.XPath("//div[contains(@class,'ui teal button')]")).Click();

            // Enter language
            IWebElement languageTextbox = driver.FindElement(By.Name("name"));
            languageTextbox.Clear();
            languageTextbox.SendKeys(language);

            // Select level
            IWebElement levelDropdown = driver.FindElement(By.Name("level"));
            levelDropdown.Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();

            // Click Add
            driver.FindElement(By.XPath("//input[@value='Add']")).Click();
            
        }

        public string GetNotificationMessageViaJS()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            string script = "return document.querySelector('.ns-box-inner')?.innerText;";
            string message = js.ExecuteScript(script) as string;

            return message;
        }


        public string GetDuplicateErrorMessage()
        {
            IWebElement message = driver.FindElement(By.XPath("//div[contains(@class,'ns-box') and contains(@class,'ns-show')]//div[@class='ns-box-inner']"));
            return message.Text;
        }

        public string GetNotificationMessage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement messageBox = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//div[contains(@class,'ns-box') and contains(@class,'ns-show')]//div[@class='ns-box-inner']")
                )
            );
            return messageBox.Text;
        }

    }
    
}

