Feature: SkillsFeature

As a Mars Project portal admin user
I would like to create, edit, and delete Skills records
So that I can manage languages records successfully

@regression

Scenario: login with valid credentials
    Given I enter valid username and password
    When I click the login button
    Then I should be logged in successfully

Scenario Outline: login with invalid credentials
    Given I enter username "<Username>" and password "<Password>"
    When I click the login button
    Then an error message should be displayed

Examples:
    | Username        | Password     |
    | abc@gmail.com  | password   |
    | abc@mail.com| password   |
    | susmitha.pinki@gmail.com  | 123123  |

Scenario Outline: Create a skill with Valid data
Given I am on the skills page
When I add a skill "<newSkill>" with level "<newLevel>"
Then the skill "<newSkill>"with Level "<newLevel>" should be created


Examples: 
| newSkill | newLevel |
| SQL      | Expert   |
| C        | Beginner |
| JAVA         |    Expert      |


Scenario Outline: Edit a Skill record
Given I am on the skills page
When I add a skill "<newSkill>" with level "<newLevel>"
And I edit the skill "<newSkill>" to "<updatedSkill>" with "<updatedLevel>"
Then the Skill record should be updated to "<updatedSkill>" with level "<updatedLevel>"

Examples: 
| newSkill | newLevel      |     updatedSkill |       updatedLevel |
| DotNet   | Beginner      |     PLSQL        |       Expert       |
|  Gherkin |   Beginner    |     Pearl        |       Expert       |



   
Scenario Outline: Delete an Skill record
  Given I am on the skills page
  When I add a skill "<newSkill>" with level "<newLevel>"
  When I delete the skill "<newSkill>"
  Then the "<newSkill>" skill record should be removed successfully

  Examples:
  | newSkill | newLevel |
  | JAVA     | Beginner  |
  | SQL     | Expert  |



Scenario Outline: prevent adding duplicate skill
    Given I login Mars portal Successfully
    When I navigate to skills page
    When I add a skill "<skill>" with level "<Level>"
    And I try to add the skill "<skill>" with level "<Level>" again
    Then a duplicate skill warning should be displayed

Examples:
    | skill | Level  |
    | Python   | Beginner |