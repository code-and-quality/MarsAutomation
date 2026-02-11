using MarsAutomation.Pages;
using MarsAutomation.Utilities;
using MarsAutomations.Pages;
using NUnit.Framework;
using OpenQA.Selenium.BiDi.Log;
using Reqnroll;
using System;

namespace MarsAutomation.StepDefinitions
{
    [Binding]
    public class SkillsFeatureStepDefinitions : CommonDriver

    {
        [When("I navigate to skills page")]
        public void WhenINavigateToSkillsPage()
        {
            HomePage HomePageObj = new HomePage(driver);
            HomePageObj.NavigateToSkills();
        }

        [Given("I am on the skills page")]
        public void GivenIAmOnTheSkillsPage()
        {

            HomePage HomePageObj = new HomePage(driver);
            HomePageObj.NavigateToSkills();
        }



        [When("I add a skill {string} with level {string}")]
        public void WhenIAddASkillWithLevel(string newSkill, string newLevel)
        {
            SkillsPage SkillsPageObj = new SkillsPage(driver);
            SkillsPageObj.AddSkill(newSkill, newLevel);
            TestDataManager.AddSkill(newSkill);
        }



        [Then("the skill {string}with Level {string} should be created")]
        public void ThenTheSkillWithLevelShouldBeCreated(string newSkill, string newLevel)
        {

            SkillsPage SkillsPageObj = new SkillsPage(driver);
            string actualSkill = SkillsPageObj.GetSkill(newSkill);
            Assert.That(actualSkill== newSkill, $"Expected Skill: {newSkill}, but got: {actualSkill}");

            string actualLevel = SkillsPageObj.GetLevel(newLevel);
            Assert.That(actualLevel == newLevel, $"Expected Level: {newLevel}, but got: {actualLevel}");
        }
        

        [When("I edit the skill {string} to {string} with {string}")]
        public void WhenIEditTheSkillToWith(string existingSkill, string newSkill, string newLevel)
        {
            SkillsPage skillPageObj = new SkillsPage(driver);
            skillPageObj.EditSkillRecord(existingSkill, newSkill, newLevel);
            TestDataManager.AddSkill(newSkill);

        }


        [Then("the Skill record should be updated to {string} with level {string}")]
        public void ThenTheSkillRecordShouldBeUpdatedToWithLevel(string newSkill, string newLevel)
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);

            string actualSkill = skillsPageObj.GetUpdatedSkill(newSkill);
            Assert.That(actualSkill == newSkill, $"Expected Skill: {newSkill}, but got: {actualSkill}");

            string actualLevel = skillsPageObj.GetUpdatedSkillsLevel(newLevel);
            Assert.That(actualLevel == newLevel, $"Expected Level: {newLevel}, but got: {actualLevel}");
        }

        [When("I delete the skill {string}")]
        public void WhenIDeleteTheSkill(string skill)
        {

            SkillsPage skillPageObj = new SkillsPage(driver);
            skillPageObj.DeleteSkill(skill);
        }


        [Then("the {string} skill record should be removed successfully")]
        public void ThenTheSkillRecordShouldBeRemovedSuccessfully(string skill)
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);
            Assert.That(skillsPageObj.IsSkillDeleted(skill),
                "Skill record was not deleted");
        }




        [When("I try to add the skill {string} with level {string} again")]
        public void WhenITryToAddTheSkillWithLevelAgain(string skill, string level)
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);
            skillsPageObj.AddDuplicateSkill(skill, level);
        }

        [Then("a duplicate skill warning should be displayed")]
        public void ThenADuplicateSkillWarningShouldBeDisplayed()
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);
            string message = NotificationHelper.GetToastMessage(driver);
            Assert.That(
             message.ToLower().Contains("already exist"),
             $"Duplicate validation message was not displayed. Actual message: '{message}'");
        }


    }

}

