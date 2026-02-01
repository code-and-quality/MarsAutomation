using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using MarsAutomations.Pages;
using NUnit.Framework;
using OpenQA.Selenium.BiDi.Log;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace MarsAutomation.StepDefinitions
{
    [Binding]
    public class LanguagesFeatureStepDefinitions : CommonDriver
    {

        [Given("I enter valid username and password")]
        public void GivenIEnterValidCredentials()
        {   
            driver = new ChromeDriver();
            LoginPage loginPage = new LoginPage();
            loginPage.EnterCredentials(driver, "susmitha.pinki@gmail.com", "123123#");
        }

        [Given(@"I enter username ""(.*)"" and password ""(.*)""")]
        public void GivenIEnterInvalidCredentials(string username, string password)
        {
            driver = new ChromeDriver();
            LoginPage loginPageObj = new LoginPage();
            loginPageObj.EnterCredentials(driver, username, password);
        }

        [When("I click the login button")]
        public void WhenIClickLogin()
        {
            LoginPage loginPageObj = new LoginPage();
            loginPageObj.ClickLogin(driver);
        }

        [Then("I should be logged in successfully")]
        public void ThenLoginShouldBeSuccessful()
        {
            HomePage homePageObj = new HomePage();
            Assert.That(homePageObj.IsLoggedIn(driver), "Login failed with valid credentials");
        }

        [Then("an error message should be displayed")]
        public void ThenErrorMessageShouldBeDisplayed()
        {
            LoginPage loginPageObj = new LoginPage();
            string message = loginPageObj.GetLoginErrorMessage(driver);

          Assert.That(
                      message.Contains("Invalid") ||
                      message.Contains("incorrect") ||
                      message.Contains("confirm") ||
                      message.Contains("email"),
                      "Expected login error message was not displayed"
     );
        }


        [Given("I login Mars portal Successfully")]
        public void GivenILoginMarsPortalSuccessfully()
        {
            driver = new ChromeDriver();
            //Login Page object initialisation and definition
            LoginPage LoginPageObj = new LoginPage();
            LoginPageObj.LoginActions(driver);

        }

        [When("I navigate to language page")]
        public void WhenINavigateToLanguagePage()
        {
            HomePage HomePageObj = new HomePage();
            HomePageObj.NavigateToLanguages(driver);
        }

        [When("I create Language record")]
        public void WhenICreateLanguageRecord()
        {
            LanguagePage LanguagePageObj = new LanguagePage(driver);
            LanguagePageObj.CreateLanguageRecord(driver);

        }

        [Then("the record should be created successfully")]
        public void ThenTheRecordShouldBeCreatedSuccessfully()
        {
            LanguagePage languagePageObj = new LanguagePage(driver);  
            
            // create record Assertions
            String newLanguage = languagePageObj.GetLanguage(driver);
            Assert.That(newLanguage == "Tulu", "Actual Language and expected Language is do not match");
            String newLevel= languagePageObj.GetLevel(driver);
            Assert.That(newLevel == "Fluent", "Actual Level and expected Level is do not match");
        }


        [When("I edit the language {string} to {string} with {string}")]
        public void WhenIEditTheLanguageToWith(string existingLangauge, string newLangauge, string newLevel)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            languagePageObj.EditLanguageRecord(driver, existingLangauge, newLangauge, newLevel);


        }

        [Then("the Language record should be updated to {string} with level {string}")]
        public void ThenTheLanguageRecordShouldBeUpdatedToWithLevel(string newLanguage, string newLevel)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);

            string actualLanguage = languagePageObj.GetUpdatedLanguage(driver, newLanguage);
            Assert.That(actualLanguage == newLanguage, $"Expected Skill: {newLanguage}, but got: {actualLanguage}");

            string actualLevel = languagePageObj.GetUpdatedLanguageLevel(driver, newLevel);
            Assert.That(actualLevel == newLevel, $"Expected Level: {newLevel}, but got: {actualLevel}");

        }



        [When("I delete the language {string}")]
        public void WhenIDeleteTheLanguage(string langauge)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            languagePageObj.DeleteLanguageRecord(driver, langauge);
       
        }


        [Then("the {string} reord should be deleted successfully")]
        public void ThenTheReordShouldBeDeletedSuccessfully(string Language)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            Assert.That(languagePageObj.IsLanguageDeleted(driver, Language),
                "Language record was not deleted");
        }





        [When("I add languages until the limit is reached")]
        public void WhenIAddLanguagesUntilLimit()
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            int currentCount = languagePageObj.GetLanguageCount(driver);

            int slotsLeft = 4 - currentCount;
            string[] languages = { "Marathi", "Italian", "Tamil", "Telugu", "Hindi", "French" };
            string[] levels = { "Fluent", "Fluent", "Conversational", "Conversational", "Basic", "Basic" };

            for (int i = 0; i < slotsLeft; i++)
            {
                languagePageObj.AddLanguage(driver, languages[i], levels[i]);
            }
        }

        [Then("the Add New button should not be visible")]
        public void ThenAddNewButtonShouldNotBeVisible()
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            Assert.That(!languagePageObj.IsAddNewButtonVisible(driver), "Add New button is still visible after reaching the limit");
        }


        [When("I try to add the language {string} with level {string} again")]
        public void WhenITryToAddTheLanguageWithLevelAgain(string language, string level)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            languagePageObj.AddDuplicateLanguage(driver, language, level);

        }



        [Then("a duplicate language warning should be displayed")]
        public void ThenADuplicateLanguageWarningShouldBeDisplayed()
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            string message = NotificationHelper.GetToastMessage(driver);
            Assert.That(
             message.ToLower().Contains("already exist"),
             $"Duplicate validation message was not displayed. Actual message: '{message}'");

           
        }

    }
}
