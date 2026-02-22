using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Log;
using OpenQA.Selenium.Support.UI;
using Reqnroll.Time;
using SeleniumExtras.WaitHelpers;
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

        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // -----------------------------
        // LOCATORS (Defined Before Methods)
        // -----------------------------

        private readonly By languagesTab = By.XPath("//a[@data-tab='first']");
        private readonly By addNewButton = By.XPath("//table[.//th[text()='Language']]//div[contains(@class,'ui teal button')]");
        private readonly By languageTextbox = By.Name("name");
        private readonly By levelDropdown = By.Name("level");
        private readonly By addButton = By.XPath("//input[@value='Add']");
        private readonly By updateButton = By.XPath("//input[@value='Update']");
        private readonly By notificationMessage = By.XPath("//div[contains(@class,'ns-box') and contains(@class,'ns-show')]//div[@class='ns-box-inner']");


        // Dynamic locators
        private By LanguageCell(string language) =>
            By.XPath($"//table[.//th='Language']//td[normalize-space()='{language}']");

        private By LevelCell(string level) =>
            By.XPath($"//table[.//th='Language']//td[normalize-space()='{level}']");

        private By EditButton(string language) =>
            By.XPath($"//tbody/tr[td[1][normalize-space()='{language}']]//i[contains(@class,'write')]");

        private By DeleteButton(string language) =>
            By.XPath($"//tbody/tr[td[1][normalize-space()='{language}']]//i[contains(@class,'remove')]");

        private By UpdatedLanguageCell(string language) =>
        By.XPath($"//table[.//th[normalize-space()='Language']]//td[normalize-space()='{language}']");

        private By UpdatedLevelCell(string level) =>
        By.XPath($"//table[.//th[normalize-space()='Language']]//td[normalize-space()='{level}']");

        private By LanguageRow(string language) =>
        By.XPath($"//tbody/tr[td[1][normalize-space()='{language}']]");



        // -----------------------------
        // CONSTRUCTOR
        // -----------------------------

        public LanguagePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        //--------------------
        //METHODS
        //--------------------

        public void GoToLanguagesTab()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(languagesTab)).Click();
        }

        public int GetLanguageCount()
        {
            var rows = driver.FindElements(By.XPath("//th[normalize-space()='Language']/ancestor::table[1]//tbody/tr"));
            return rows.Count;
        }

        public void AddLanguage(string language, string level)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(addNewButton)).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(languageTextbox)).SendKeys(language);

            wait.Until(ExpectedConditions.ElementToBeClickable(levelDropdown)).Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(addButton)).Click();
        }

        public string GetLanguage(string language)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(LanguageCell(language))).Text;
        }

        public string GetLevel(string level)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(LevelCell(level))).Text;
        }

        public void EditLanguage(string existingLanguage, string newLanguage, string newLevel)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(EditButton(existingLanguage))).Click();

            var textbox = wait.Until(ExpectedConditions.ElementIsVisible(languageTextbox));
            textbox.Clear();
            textbox.SendKeys(newLanguage);

            wait.Until(ExpectedConditions.ElementToBeClickable(levelDropdown)).Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{newLevel}']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(updateButton)).Click();
        }

        public string GetUpdatedLanguage(string expectedLanguage)
        {
            var element = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    UpdatedLanguageCell(expectedLanguage)
                )
            );

            return element.Text;
        }

        public string GetUpdatedLanguageLevel(string expectedLevel)
        {
            var element = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    UpdatedLevelCell(expectedLevel)
                )
            );

            return element.Text;
        }


        public void DeleteLanguageRecord(string languageToDelete)
        {
            GoToLanguagesTab();

            var deleteBtn = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    DeleteButton(languageToDelete)
                )
            );

            deleteBtn.Click();
        }





        public void DeleteLanguage(string language)
        {
            var deleteButtons = driver.FindElements(DeleteButton(language));

            if (deleteButtons.Count == 0)
                return; // Already deleted, nothing to do

            wait.Until(ExpectedConditions.ElementToBeClickable(deleteButtons[0])).Click();
        }

        public bool IsLanguageDeleted(string language)
        {
            return driver.FindElements(LanguageRow(language)).Count == 0;
        }

        public void AddDuplicateLanguage(string language, string level)
        {
            GoToLanguagesTab();

            wait.Until(ExpectedConditions.ElementToBeClickable(addNewButton)).Click();

            var textbox = wait.Until(ExpectedConditions.ElementIsVisible(languageTextbox));
            textbox.Clear();
            textbox.SendKeys(language);

            wait.Until(ExpectedConditions.ElementToBeClickable(levelDropdown)).Click();
            driver.FindElement(By.XPath($"//option[normalize-space()='{level}']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(addButton)).Click();
        }


        public bool IsAddNewButtonVisible()
        {
            var buttons = driver.FindElements(addNewButton);
            return buttons.Count > 0 && buttons[0].Displayed;
        }

        public string GetNotificationMessage()
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(notificationMessage)).Text;
        }

    }
}

       