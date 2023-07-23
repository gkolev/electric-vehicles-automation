Feature: CarsFeature

@User
Scenario: GetCars endpoint is requested
	Given 5 cars are created and saved into db
	When GetCars endpoint is requested
	Then The status code of the response should be 200
		And Response should contain the requested cars

Scenario: GetCars endpoint is requested with wrong user
	Given 5 cars are created and saved into db
	When GetCars endpoint is requested with wrong user
	Then The status code of the response should be 401

@User
Scenario: GetCar endpoint is requested
	Given 1 cars are created and saved into db
	When GetCar endpoint is requested
	Then The status code of the response should be 200
		And Response should contain the requested car
