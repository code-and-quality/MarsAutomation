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

Scenario: create a skills record with valid data
	Given I login Mars portal Successfully
	When I navigate to skills page 
	When I create skills record
	Then the Skill record should be created successfully

Scenario Outline: edit a specific skill record
    Given I login Mars portal Successfully
    When I navigate to skills page
    And I edit the skill "<existingSkill>" to "<newSkill>" with "<newLevel>"
    Then the Skill record should be updated to "<newSkill>" with level "<newLevel>"


    
Examples:
    | existingSkill| newSkill   | newLevel          |
    | JAVA   | DotNet | Expert |
    | HTML | Python | Beginner         |

Scenario Outline: delete an existing skill record
    Given I login Mars portal Successfully
    When I navigate to skills page
    And I delete the Skill "<Skill>"
    Then the "<Skill>" record should be removed successfully

Examples:
    | Skill |
    | JAVA  |

Scenario Outline: prevent adding duplicate skill
    Given I login Mars portal Successfully
    When I navigate to skills page
    And I try to add the skill "<skill>" with level "<Level>" again
    Then a duplicate skill warning should be displayed

Examples:
    | skill | Level  |
    | Python   | Beginner |