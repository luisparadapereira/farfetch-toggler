
# farfetch-toggler



### Requirements (as setup currently):
- Frontend
	- [NodeJS](https://nodejs.org/en/download/) v8.8.1 
	- NPM v4.1.2
	 - [Angular CLI](https://github.com/angular/angular-cli) 1.1.2
    ```
    npm install -g @angular/cli
    ```
    - [Gulp CLI](https://gulpjs.com/) v1.3.0
    ```
    npm install -g gulp-cli
    ```
    - [Gulp](https://gulpjs.com/) v3.9.1
    ```
    npm install -g gulp
    ```
    - Http Server -
    ```
    npm install -g http-server
    ```
    - TypeScript
    ```
    npm install -g typescript
    ```
- Backend
	- [MongoDB](https://docs.mongodb.com/manual/tutorial/install-mongodb-on-windows/)
	- [.NetCore 2](https://www.microsoft.com/net/download/windows)
	- RabbitMQ 
	  - [Erlang](http://www.erlang.org/downloads)
	  - [RabbitMQ](http://www.rabbitmq.com/install-windows.html)

### Build & Settings
- Frontend
	- Should build automatically when running.
- Backend
	- Open the VisualStudio solution and build the projects under Release
		- Farfetch.RestAPI
		- Farfetch.PlusApp
		- Farfetch.DataInsertion
	- RabbitMQ was used with predefined settings.

### Running
- There are three Windows PowerShell scripts to make it easier to run the application.
	- START_DATAINSERTION: Will prefill some data into the Mongo database
	- START_API: Will run the API from the project's release directory into http://localhost:5000
	- START_TESTSERVICE: Will run the test service from the project's release directory into a console
	- START_CLIENT: Will build the angular application and run it to: http://localhost:4200

### Usage
* First you should run the data insertion application in order to have some prefilled data to login into the application
* Start all the other scripts a wait for the client's build to be complete. 
* You should be able to go into http://localhost:4200 and enter the application with the following data
	* username: admin
	* password: admin
* Once you log in, as admin, you have access to three areas:
	* Manage Users [Only admins can access]
		* Edit user profiles 
		* Deletes users
	* Manage Toggles
		* View all the toggles
		* By clicking on a toggle you can edit it or delete it
			* Select the services to associate the toggle with them
		* By cliking on "Register a new toggle" you create a new toggle
	* Manage Services
		* View all the services
		* Edit the service API Key
		* Register a new service
* You should see a service named Farfetch.PlusApp with three toggles associated with it:
	* toggle1 with value true
	* toggle1 with value false and override to true
	* toggle2 with value true
* Changing these values should reflect in the console window connected to the TestService.
* When a user registers he will automatically have his profile set to Public

### Authorizations
|  | Admin | Developer | Public |
|--|-------|-----------|--------|
| Edit users |:white_check_mark:  | :x: | :x:
| Delete users |:white_check_mark:  | :x: | :x:
| View Toggles |:white_check_mark:|:white_check_mark:|:white_check_mark:
| Edit Toggles |:white_check_mark: | :white_check_mark: | :x:
| Delete Toggles |:white_check_mark: | :white_check_mark: | :x: 
| View Services |:white_check_mark: | :white_check_mark: | :x: 
| Edit Services |:white_check_mark: | :white_check_mark: | :x: 
| Insert New Services |:white_check_mark: | :white_check_mark: | :x: 
| Delete Services |:white_check_mark: | :white_check_mark: | :x: 


### Known Bugs
**Problem:** Windows Powershell isn't ouputting any information when I change the toggles associated with Farfetch.PlusApp
**Solution:** As weird as it sounds, click on the console and press enter a few times. It should do the trick since this has to do with some focus issue and the quick edit mode that is turned on by default.

### Technical Specifications
#### Main Design Patterns 
* Repository Pattern
* Core Unit of Work
* Dependency Injection
* Factory Pattern
#### Main Features
* Allows for new database integrations. All that is necessary is create the repository of that database
* 

### Next Steps
* Drastically improve the user table. Currently is not encrypting the password and it should using something like a SHA512 algorithm. 
* 

### Development Timeline
* Architecture Layout - 0.5 day
* Base Architecture Implementation - 0.5 days
* User and Service Authentication - 0.75 days
* Messaging service research - 0.25 days
* Frontend development - 1.25 days
* Messaging service implementation - 0.25 days
* Finishing touches - 0.5 days
* Tests and documentation - 1.5 days



