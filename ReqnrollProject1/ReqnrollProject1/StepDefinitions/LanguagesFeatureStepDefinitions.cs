using MarsAutomation.Hooks;
using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using MarsAutomations.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
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
        private readonly IWebDriver driver;
        private LoginPage loginPage;
        private HomePage homePage;

        public LanguagesFeatureStepDefinitions(CommonDriver commonDriver)
        {
            driver = commonDriver.driver;
            loginPage = new LoginPage(driver);
        }


        [Given(@"I enter valid username and password")]
        public void GivenIEnterValidUsernameAndPassword()
        {
            loginPage = new LoginPage(driver);
            loginPage.NavigateToPortal();
            loginPage.ClickSignIn();

            loginPage.EnterUsername("susmitha.pinki@gmail.com");
            loginPage.EnterPassword("123123#");
        }

        [Given(@"I enter username ""([^""]*)"" and password ""([^""]*)""")]
        public void GivenIEnterUsernameAndPassword(string username, string password)
        {
            loginPage = new LoginPage(driver);
            loginPage.NavigateToPortal();
            loginPage.ClickSignIn();

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
        }

        //When

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            loginPage.ClickLogin();
        }

        //Then

        [Then(@"I should be logged in successfully")]
        public void ThenIShouldBeLoggedInSuccessfully()
        {
            homePage = new HomePage(driver);
            Assert.That(homePage.IsLoggedIn(), Is.True,
                 "Login failed: user is not logged in successfully");
        }

        [Then(@"an error message should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed()
        {
            string errorMessage = loginPage.GetLoginErrorMessage();
            Assert.That(errorMessage, Is.Not.Empty,
          "Expected error message was not displayed for invalid login");
        }


        [Given("I login Mars portal Successfully")]
        public void GivenILoginMarsPortalSuccessfully()
        {
            //No need to add Code here-- Hooks already logged in
            loginPage = new LoginPage(driver);
            loginPage.NavigateToPortal();
            loginPage.ClickSignIn();

            loginPage.EnterUsername("susmitha.pinki@gmail.com");
            loginPage.EnterPassword("123123#");
            loginPage.ClickLogin();


        }

        [When("I navigate to language page")]
        public void WhenINavigateToLanguagePage()
        {
           
            HomePage HomePageObj = new HomePage(driver);
            HomePageObj.NavigateToLanguages();
        }

     

        [Given("I am on the language page")]
        public void GivenIAmOnTheLanguagePage()
        {
            loginPage = new LoginPage(driver);
            loginPage.NavigateToPortal();
            loginPage.ClickSignIn();

            loginPage.EnterUsername("susmitha.pinki@gmail.com");
            loginPage.EnterPassword("123123#");
            loginPage.ClickLogin();
            HomePage HomePageObj = new HomePage(driver);
            HomePageObj.NavigateToLanguages();
        }

        [When("I create a language {string} with level {string}")]
        public void WhenICreateALanguageWithLevel(string newLanguage, string newLevel)
        {
            LanguagePage LanguagePageObj = new LanguagePage(driver);
            LanguagePageObj.AddLanguage(newLanguage, newLevel);
            TestDataManager.AddLanguage(newLanguage);

        }

        [Then("the language {string}with Level {string} should be created")]
        public void ThenTheLanguageWithLevelShouldBeCreated(string newLanguage, string newLevel)
        {

            LanguagePage languagePageObj = new LanguagePage(driver);
            string actualLanguage = languagePageObj.GetLanguage(newLanguage);
            Assert.That(actualLanguage == newLanguage, $"Expected Skill: {newLanguage}, but got: {actualLanguage}");

            string actualLevel = languagePageObj.GetLevel(newLevel);
            Assert.That(actualLevel == newLevel, $"Expected Level: {newLevel}, but got: {actualLevel}");
        }



        [When("I add a language {string} with level {string}")]
        public void WhenIAddALanguageWithLevel(string newLanguage, string newLevel)
        {
            LanguagePage LanguagePageObj = new LanguagePage(driver);
            LanguagePageObj.AddLanguage(newLanguage, newLevel);
            TestDataManager.AddLanguage(newLanguage);

        }

        [When("I update the language {string} to {string} with level {string}")]
        public void WhenIUpdateTheLanguageToWithLevel(string existingLanguage, string updatedLanguage, string updatedLevel)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            languagePageObj.EditLanguage(existingLanguage, updatedLanguage,updatedLevel);
            // Track the updated language for cleanup
            TestDataManager.AddLanguage(updatedLanguage);



        }

        [Then("the language {string} with level {string} should be displayed")]
        public void ThenTheLanguageWithLevelShouldBeDisplayed(string updatedLanguage, string updatedLevel)
        {
            LanguagePage languagePageObj = new LanguagePage(driver); 
            string actualLanguage = languagePageObj.GetUpdatedLanguage(updatedLanguage);
            Assert.That(actualLanguage == updatedLanguage, $"Expected Skill: {updatedLanguage}, but got: {actualLanguage}");

            string actualLevel = languagePageObj.GetUpdatedLanguageLevel(updatedLevel);
            Assert.That(actualLevel == updatedLevel, $"Expected Level: {updatedLevel}, but got: {actualLevel}");
        }




        [When("I delete the language {string}")]
        public void WhenIDeleteTheLanguage(string langauge)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            languagePageObj.DeleteLanguageRecord(langauge);
       
        }


        [Then("the {string} reord should be deleted successfully")]
        public void ThenTheReordShouldBeDeletedSuccessfully(string Language)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            Assert.That(languagePageObj.IsLanguageDeleted(Language),
                "Language record was not deleted");
        }



        [When("I add languages until the limit is reached")]
        public void WhenIAddLanguagesUntilLimit()
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            int currentCount = languagePageObj.GetLanguageCount();

            int slotsLeft = 4 - currentCount;
            string[] languages = { "Marathi", "Italian", "Tamil", "Telugu", "Hindi", "French" };
            string[] levels = { "Fluent", "Fluent", "Conversational", "Conversational", "Basic", "Basic" };

            for (int i = 0; i < slotsLeft; i++)
            {
                languagePageObj.AddLanguage(languages[i], levels[i]);
            }
        }

        [Then("the Add New button should not be visible")]
        public void ThenAddNewButtonShouldNotBeVisible()
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            Assert.That(!languagePageObj.IsAddNewButtonVisible(), "Add New button is still visible after reaching the limit");
        }


        [When("I try to add the language {string} with level {string} again")]
        public void WhenITryToAddTheLanguageWithLevelAgain(string language, string level)
        {
            LanguagePage languagePageObj = new LanguagePage(driver);
            languagePageObj.AddDuplicateLanguage(language, level);

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
