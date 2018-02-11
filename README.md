
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
	- Some settings are exposed at root directory level for easier access. **Build the application first**.

### Running
- There are three Windows PowerShell scripts to make it easier to run the application.
	- START_DATAINSERTION: Will prefill some data into the Mongo database
	- START_API: Will run the API from the project's release directory into http://localhost:5000
	- START_TESTSERVICE: Will run the test service from the project's release directory into a console
	- START_CLIENT: Will build the angular application and run it to: http://localhost:4200

### Usage
#### Authorizations
|  | Section | Admin | Developer | Public |
|--|--|-------|-----------|--------|
| Edit users |Manage users|:white_check_mark:  | :x: | :x:
| Delete users |Manage users|:white_check_mark:  | :x: | :x:
| View Toggles |Manage toggles|:white_check_mark:|:white_check_mark:|:white_check_mark:
| Edit Toggles |Manage toggles|:white_check_mark: | :white_check_mark: | :x:
| Delete Toggles |Manage toggles|:white_check_mark: | :white_check_mark: | :x: 
| View Services |Manage services|:white_check_mark: | :white_check_mark: | :x: 
| Edit Services |Manage services|:white_check_mark: | :white_check_mark: | :x: 
| Insert New Services |Manage services|:white_check_mark: | :white_check_mark: | :x: 
| Delete Services |Manage services|:white_check_mark: | :white_check_mark: | :x: 
| Log out ||:white_check_mark:|:white_check_mark:|:white_check_mark:


#### Data filling
Run the Data Insertion application first. This will create the admin account, necessary toggles and services to correctly show how the messaging works.

***Available Users***

| Username | Password | Profile |
| -- | -- | -- | 
|  admin | admin | Admin |
|  developer | developer | Developer |
|  public | public | Public |


#### Front end 
* Go into http://localhost:4200
* Log in with the following credentials
	* username: admin
	* password: admin
* You will  be able to manage user profiles, create toggles and services
* When a user registers he will automatically be assigned the Public profile

#### Farfetch.PlusApp
Two main functions
* *Plus* - Adds 2 to the input number
* *Mult* - Multiplies the input number by 2

Example behaviours

| Input| Action | Toggle Name | Toggle Value | Output |
|--|-- | -- | -- | -- |
|  4| Plus | toggle1 | true |  6 |
|  4| Plus | toggle1 | false|  4 |
|  4| Mult | toggle2 | true|  8 |
|  4| Mult | toggle2 | false|  4 |

Note that if you have two toggle1 with different values, the one with Overrides set to true with be the default value.

### Known Bugs
**Problem:** Windows Powershell isn't ouputting any information when I change the toggles associated with Farfetch.PlusApp
**Solution:** As weird as it sounds, click on the console and press enter a few times. It should do the trick since this has to do with some focus issue and the quick edit mode that is turned on by default.

### Technical Specifications
#### Api Documentation
Run the Farfetch.RestAPI and go to: 
http://localhost:5000/swagger

#### Test Coverage
Open the file Farfetch.TestCoverage.html located at the root of the project for coverage information

#### Main Design Patterns 
* Repository Pattern
	* Each DB has a repository that directly interacts with it (Currently we have MongoDB setup.)
* Core Unit of Work
	* The core unit of work will allow the services that interact with the database to use only one data structure that will always respect the same contract. Allows for easier change of database engines.
* Dependency Injection
	* Used throughout the application. Easily seen in the configuration files injected from the WebAPI all the end to the Core Unit with the dbSettings file. Makes each module of the application independent for the rest.
* Factory Pattern
	* Used to get applications and Apis. 
* Broadcast/Subscriber 
	* Used to transmit and receive messages
#### Main Features
* Allows for new database integrations. All that is necessary is create the repository of that database
* JWT Encoding for User tokens and Service API Tokens
* Authentication role guards in the front end
* Authentication role guards in the backend (can be an overkill but the frontend is easily hacked)
* RabbitMQ messaging system allows for a truly autonomous REST API. 
* Event driven update on client application

### Next Steps
* Drastically improve the user table. Currently is not encrypting the password and it should using something like a SHA512 algorithm. 
* Review the messaging platform. This is just a prototype that uses Rabbit in the easiest way possible. The will be a lot of room for improvement.
* Review the front end. This is just a prototype and in terms of code efficiency can be improved.
* Improve test coverage. Currently we're at 70% average. (Missing mainly exception throwing tests)

### Development Timeline
* Architecture Layout - 0.5 day
* Base Architecture Implementation - 0.5 days
* User and Service Authentication - 0.75 days
* Messaging service research - 0.25 days
* Front end development - 1.25 days
* Messaging service implementation - 0.25 days
* Finishing touches - 0.5 days
* Tests and documentation - 1.5 days



