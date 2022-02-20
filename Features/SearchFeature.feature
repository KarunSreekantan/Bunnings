Feature: SearchFeature
	Validate the Bunnings Search feature

@mytag
Scenario:  Bunnings Customers can search products on Bunnings website
	Given  Customer launches Bunnings website
	And    customer enters the search term "Ozito 18v" on the bunnings website search field and click on the search button
	Then   Customer is displayed Products that matches the search term "Ozito 18v"


	Examples: 
	| Browser |
	| chrome  |
	| firefox |


@mytag
Scenario: Bunnings Customer is suggested products based on search term Entered
Given  Customer launches Bunnings website
And    customer enters the search term "Ryobi" on the bunnings website search field
Then   Customer is displayed a list of product sugesstions for the search term "Ryobi" 
	
	Examples: 
	| Browser |
	| chrome  |
	| firefox |



@mytag
Scenario: Bunnings customer can find further details regarding flybuys points on the search dialogue
Given  Customer launches Bunnings website
Then   Customer can find out more information regarding Flybuys points by clicking on the Find out Banner

	Examples: 
	| Browser |
	| chrome  |
	| firefox |