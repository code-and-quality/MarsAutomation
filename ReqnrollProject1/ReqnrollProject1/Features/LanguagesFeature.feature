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

Scenario: create a language record with valid data
	Given I login Mars portal Successfully
	When I navigate to language page 
	When I create Language record
	Then the record should be created successfully

Scenario Outline: edit a specific language record
    Given I login Mars portal Successfully
    When I navigate to language page
    And I edit the language "<existingLanguage>" to "<newLanguage>" with "<newLevel>"
     Then the Language record should be updated to "<newLanguage>" with level "<newLevel>"

Examples:
    | existingLanguage| newLanguage    | newLevel          |
    | Tamil    | Kannada | Conversational |
    | English  | French  | Basic          |


 Scenario Outline: delete an existing language record
    Given I login Mars portal Successfully
    When I navigate to language page
    And I delete the language "<Language>"
    Then the "<Language>" reord should be deleted successfully


Examples:
    | Language | 
    | Kannada  |

Scenario: User cannot add more than four languages
    Given I login Mars portal Successfully
    When I navigate to language page
    And I add languages until the limit is reached
    Then the Add New button should not be visible

Scenario Outline: prevent adding duplicate language
    Given I login Mars portal Successfully
    When I navigate to language page
    And I try to add the language "<Language>" with level "<Level>" again
    Then a duplicate language warning should be displayed

Examples:
    | Language | Level  |
    | French   | Basic  |
