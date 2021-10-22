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

	- InterviewExcercise
		- ApiClient -> Classes with RestClient with respective endpoints, requests and responses
		- Tests -> Test classes separated by functionalities and respecting atomicity of tests
		- Reporter -> Classes with Reporting and logging mechanisms

## How to execute it 

 1. Install Net core 5
 2. execute ''dotnet test'' in console standing on folder where is .sln file
 3. Html Report will be automatically created on the base folder under the ExtentReport.html name
 4. If you want to print logs in console too run it with ''dotnet test --logger "console;verbosity=detailed"'' 