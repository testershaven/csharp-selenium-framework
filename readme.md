# Interview Challenge

## Assignment

See the sample REST API at: https://gorest.co.in/ 

You can manually create an access token on this site to later use in tests.

With the tooling of your choice, create a test suite for this API to test at least the following:

- Creating a new user, post, comment and todo. Test for success scenarios and also for when some mandatory fields are missing.
- Test if you can create two users with the same email address.
- Test if the endpoints allow you to create new entries with invalid email address formats.
- Fetch the entries that you created, test that the returned data matches your input.

## Project structure

Net core project is divided in the followings

	- docker
		- config.toml -> has configuration for selenium nodes
		- docker-compose.yml -> orchestrator for hub and nodes
	- InterviewExcercise
		- ApiClient -> Classes with RestClient with respective endpoints, requests and responses
		- Tests -> Test classes separated by functionalities and respecting atomicity of tests
		- Reporter -> Classes with Reporting and logging mechanisms
		- UiClient -> Classes with SeleniumClient.

## How to execute it 

 1. Install Net core 5
 2. Have Chrome installed
 3. Execute ''dotnet test'' in console standing on folder where is .sln file
 4. Html Report will be automatically created on the base folder under the ExtentReport.html name
 5. If you want to print logs in console too run it with ''dotnet test --logger "console;verbosity=detailed"''

 ## How to execute selenium grid

 1. Install docker
 2. In terminal move to docker folder
 3. run 'docker compose up'
 4. Open appsettings.json
 5. Set browser parameter as 'RemoteWebDriver'
 6. Execute ''dotnet test'' in console standing on folder where is .sln file