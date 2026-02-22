Feature: LanguagesFeature

As a Mars Project portal admin user
I would like to create, edit, and delete languages records
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

Scenario Outline: Create a language with Valid data
Given I am on the language page
When I create a language "<newLanguage>" with level "<newLevel>"
Then the language "<newLanguage>"with Level "<newLevel>" should be created


Examples: 
| newLanguage    | newLevel |
| Tulu           |    Basic      |
| Telugu          |   Fluent     |



Scenario Outline: Update a language and level
  Given  I am on the language page
  When I add a language "<newLanguage>" with level "<newLevel>"
  And I update the language "<newLanguage>" to "<updatedLanguage>" with level "<updatedLevel>"
  Then the language "<updatedLanguage>" with level "<updatedLevel>" should be displayed

Examples:
  | newLanguage | newLevel | updatedLanguage | updatedLevel |
  | English     | Basic    | French          | Fluent       |
  |   Hindi          |   Basic       |    Malayalam             |    Fluent          |


Scenario Outline: Delete an existing language record
  Given I am on the language page
  When I add a language "<newLanguage>" with level "<newLevel>"
  When I delete the language "<newLanguage>"
  Then the "<newLanguge>" reord should be deleted successfully

  Examples:
  | newLanguage | newLevel |
  | Tamil       | Fluent |
  | Telugu      | Basic   |



Scenario: User cannot add more than four languages
    Given I login Mars portal Successfully
    When I navigate to language page
    And I add languages until the limit is reached
    Then the Add New button should not be visible

Scenario Outline: prevent adding duplicate language
    Given I login Mars portal Successfully
    When I navigate to language page
    When I create a language "<Language>" with level "<Level>"
    And I try to add the language "<Language>" with level "<Level>" again
    Then a duplicate language warning should be displayed

Examples:
    | Language | Level  |
    | French   | Basic  |
