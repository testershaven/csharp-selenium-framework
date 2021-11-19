# Net Standard Testing Framework Template

## Features

	- ApiClient to test endpoints
	- UiClient based on Selenium to test webpages
		- Chrome, Firefox and Edge supported
		- RemoteWebDriverSupported
		- Dynamic Selenium grid
		- Dockerized enviroment support
	- Parallel execution support
	- Custom Html Report highly cutomizable

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

### Local Execution

 1. Install Net core 5
 2. Have Chrome installed
 3. Execute ''dotnet test'' in console standing on folder where is .sln file
 4. Html Report will be automatically created on the base folder under the ExtentReport.html name
 5. If you want to print logs in console too run it with ''dotnet test --logger "console;verbosity=detailed"''

### Remote Execution (Selenium grid / Docker)

 1. Install docker
 2. In terminal move to docker folder
 3. run 'docker compose up'
 4. Open appsettings.json
 5. Set isRemote parameter as 'true'
 6. Execute ''dotnet test'' in console standing on folder where is .sln file

## Next Futures

	- Add Database Support