Feature: Categories
	In order to register categories
	I want to receive the description and create a category.

@addtag
Scenario: Add categories
	Given I have entered a 'description' into the category form
	When I press add
	Then the result should be 'Category added successfully.' on the screen