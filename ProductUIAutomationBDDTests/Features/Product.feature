Feature: Product
	Test the Product Page functionalities

Background:
	Given Cleanup following data
		| Name			| Description			| Price	| ProductType |
		| Headphone		| Wireless Headphone	| 3500	| EXTERNAL    |
		| Monitor		| HD Monitor			| 5000	| MONITOR	  |

@sanity @retry(1)
Scenario: Create product and verify details
	Given Click on Product menu
	And Click on Create link
	When Create product with following details
		| Name			| Description			| Price | ProductType |
		| Headphone		| Wireless Headphone	| 3500	| EXTERNAL    |
	And Click on Details link of the newly created product
	Then All the product details are created as expected

@sanity
Scenario: Edit Product and verify if it is updated
	Given Ensure the following product is created
		| Name			| Description			| Price	| ProductType |
		| Monitor		| HD Monitor			| 5000	| MONITOR	  |
	And Click on Product menu
	When Click on Edit link of the newly created product
	And Edit the product with following details
		| Name			| Description           | Price | ProductType |
		| Monitor		| Full HD Monitor		| 9000	| MONITOR	  |
	And Click on Details link of the newly created product
	Then All the product details are created as expected