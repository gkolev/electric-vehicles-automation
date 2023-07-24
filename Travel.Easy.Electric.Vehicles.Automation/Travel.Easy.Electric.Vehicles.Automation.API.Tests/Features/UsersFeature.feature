Feature: UsersFeature

Scenario: PostUserRegister endpoint is requested
	Given UserRegisterRequestModel is created
	When PostRegisterUser endpoint is requested
	Then The status code of the response should be 201
		And User should be correctly added into db
