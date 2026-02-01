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
            HomePage HomePageObj = new HomePage();
            HomePageObj.NavigateToSkills(driver);
        }



        [When("I create skills record")]
        public void WhenICreateSkillsRecord()
        {
           
            SkillsPage SkillsPageObj = new SkillsPage(driver);
            SkillsPageObj.CreateSkillsRecord(driver);
        }



        [Then("the Skill record should be created successfully")]
        public void ThenTheSkillRecordShouldBeCreatedSuccessfully()
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);

            // create record Assertions
            String newSkill = skillsPageObj.GetSkill(driver);
            Assert.That(newSkill == "JAVA", "Actual Skill and expected Skill is do not match");
            String newLevel = skillsPageObj.GetLevel(driver);
            Assert.That(newLevel == "Expert", "Actual Level and expected Level is do not match");
        }



        [When("I edit the skill {string} to {string} with {string}")]
        public void WhenIEditTheSkillToWith(string existingSkill, string newSkill, string newLevel)
        {
            SkillsPage skillPageObj = new SkillsPage(driver);
            skillPageObj.EditSkillRecord(driver, existingSkill, newSkill, newLevel);
        }


        [Then("the Skill record should be updated to {string} with level {string}")]
        public void ThenTheSkillRecordShouldBeUpdatedToWithLevel(string newSkill, string newLevel)
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);

            string actualSkill = skillsPageObj.GetUpdatedSkill(driver, newSkill);
            Assert.That(actualSkill == newSkill, $"Expected Skill: {newSkill}, but got: {actualSkill}");

            string actualLevel = skillsPageObj.GetUpdatedSkillsLevel(driver, newLevel);
            Assert.That(actualLevel == newLevel, $"Expected Level: {newLevel}, but got: {actualLevel}");
        }



        [When("I delete the Skill {string}")]
        public void WhenIDeleteTheSkill(string skill)
        {
            SkillsPage skillPageObj = new SkillsPage(driver);
            skillPageObj.DeleteSkillRecord(driver, skill);
        }


        [Then("the {string} record should be removed successfully")]
        public void ThenTheDeletedSkillRecordShouldBeRemovedSuccessfully(string Skill)
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);
            Assert.That(skillsPageObj.IsSkillDeleted(driver, Skill),
                "Skill record was not deleted");
        }

        [When("I try to add the skill {string} with level {string} again")]
        public void WhenITryToAddTheSkillWithLevelAgain(string skill, string level)
        {
            SkillsPage skillsPageObj = new SkillsPage(driver);
            skillsPageObj.AddDuplicateSkill(driver, skill, level);
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

