## C# .NET Selenium Project with BDD SpecFlow Tests

This repository contains a C# .NET based Selenium project that demonstrates automated testing of a web application using Selenium WebDriver. It incorporates Behavior-Driven Development (BDD) principles using SpecFlow for writing tests in a human-readable format. It also integrates various tools and frameworks to enhance test automation, logging and reporting capabilities. The project includes the following components:

**Selenium WebDriver**: A powerful framework for automating web browsers.

**Selenium Support**: Additional support libraries and dependencies that can enhance the functionality and usability of Selenium WebDriver.

**SeleniumExtras Wait Helpers**: Additional wait helper classes for Selenium WebDriver to enhance synchronization in tests.

**SpecFlow**: A framework for Behavior-Driven Development (BDD) that enables defining and executing acceptance tests in Gherkin format.

**xUnit**: A testing framework for .NET that provides a simple and extensible approach to writing automated tests.

**AutoFixture**: A library for generating test data automatically, making it easier to write automated tests with less test data setup.

**Fluent Assertions**: A library that provides a more fluent and readable approach to asserting test results.

**Serilog**: A flexible logging framework that allows structured logging with various output sinks.

**SpecFlow LivingDoc**: A tool that generates living documentation from SpecFlow tests, providing an overview of test results and specifications.

**Allure Reporting**: An open-source framework for generating test reports with rich features like attachments, tags, and historical trends.

**Extent Reporting**: A customizable reporting framework that provides detailed test reports with rich visualization.

**Docker**: A platform that enables building, deploying, and running applications using containerization.

<br>

## Pre-requisites

* .NET 6.0 SDK
* Visual Studio 2022 (or any other compatible IDE)
* Docker

<br>

## Setup

1. Clone the repository to your local machine:

    `git clone <repository-url>`

2. Navigate to the project directory where the \<*Solution*>.sln file is present.

3. Run the application under test and database by following the steps mentioned in *Visual Studio* section under *Running Tests*.

4. Open the \<*Solution*>.sln file in Visual Studio or in your preferred IDE.

5. Install the required NuGet packages by restoring the dependencies for the solution.

6. Configure the test settings in appsettings.\<*environment*>.json file in the project if you want to change the default values. For example, browser type, timeout interval, other relevant settings as per your requirements (Optional).  

        Environments:
            appsettings.local.json -> test settings for running the tests locally
            appsettings.remote.json -> test settings for running the tests inside docker container

7. Build the solution to ensure all the necessary binaries are generated.

8. Run the tests from the Test Explorer  

<br>

## Running Tests

**Visual Studio**

1. Navigate to project's root directory where the \<*Solution*>.sln file is present.  

2. Open docker-compose.yml file in your preferred editor.  

3. Keep the services (*product_api*, *product_webapp*), corresponding to the Docker containers of the application under test, and the service (*sql_db*), corresponding to the Docker container of the database uncommented and comment out rest of the services.  

4. Open a terminal or command prompt at the above docker-compose.yml file location.  

5. Execute the following command to build the services (defined in docker-compose.yml file):

    `dotnet build`  

6. After the services are built, execute the following command to create and start the containers (defined in docker-compose.yml file):

    `docker-compose up`

7. After the containers (defined in docker-compose.yml file) are up and running, open the Test Explorer in Visual Studio.  

8. You can run the tests from the Test Explorer.

**CLI**

1. Navigate to project's root directory where the \<*Solution*>.sln file is present.  

2. Open docker-compose.yml file with your preferred editor.  

3. Keep the services (*product_api*, *product_webapp*), corresponding to the Docker containers of the application under test, and the service (*sql_db*), corresponding to the Docker container of the database uncommented and comment out rest of the services.  

4. Open a terminal or command prompt at the above docker-compose.yml file location.  

5. Execute the following command to build the services (defined in docker-compose.yml file):
    
    `dotnet build`  

6. After the services are built, execute the following command to create and start the containers (defined in docker-compose.yml file):
    
    `docker-compose up`

7. After the containers (defined in docker-compose.yml file) are up and running, execute the following command to run the tests:
    
    `dotnet test`

**Docker**

1. Navigate to project's root directory where the \<*Solution*>.sln file is present.  

2. Open docker-compose.yml file with your preferred editor.  

3. Keep the services as is.

4. Open a terminal or command prompt at the above docker-compose.yml file location.  

5. Execute the following command to build the services (defined in docker-compose.yml file):
    
    `docker-compose build`

6. After the services are built, execute the following command to create and start the containers (defined in docker-compose.yml file):
    
    `docker-compose up`

The tests will be executed inside the Docker container (*producttest*), as defined in docker-compose.yml file, and the test results (execution logs, execution reports, execution recorded videos) will be generated in the output folder as mentioned in the configuration after the completion of the test execution.

<br>

## Logging & Reporting

**Serilog**  

Serilog enables comprehensive logging during the execution of tests. It allows you to capture log messages with context-rich information, making it easier to troubleshoot issues and analyze test results. The logs can be found in the output folder as defined in the configuration, after the test execution. You can open the generated logs in your preferred editor.

*This project supports three types of reporting:*

**Extent Reporting**  

The Extent Reporting library provides rich and interactive HTML report, that can be found in the output folder as defined in the configuration, after the test execution. You can open the generated Extent report in your preferred browser.

**Allure Reporting**  

The Allure Reporting library generates interactive and visually appealing reports. To generate the Allure report, execute the following command using CLI:  

`allure serve <generated_report_path>`  

You can open the generated Allure report in your preferred browser.

**SpecFlow LivingDoc**  

SpecFlow LivingDoc is used to generate living documentation based on the feature files and their associated scenarios. To generate the living documentation from your SpecFlow tests, execute the following command using CLI:  

`livingdoc feature-folder <path_to_test_project> -t <path_to_test_project>\bin\debug\net<version>\TestExecution.json`  

You can open the generated LivingDoc report in your preferred browser.

<br>

## Continuous Integration (CI)

This project can be integrated with a CI/CD pipeline of your choice, such as Jenkins. Configure the pipeline to build the project, restore dependencies, execute tests, and generate the required reports.

<br>

## Additional Information

For more details on the usage of the various tools and frameworks incorporated in this project, refer to their respective documentations:

[Selenium WebDriver](https://www.selenium.dev/documentation/webdriver/)  
[SpecFlow](https://specflow.org/)  
[xUnit](https://xunit.net/)  
[AutoFixture](https://github.com/AutoFixture/AutoFixture) 
[Fluent Assertions](https://fluentassertions.com/)   
[Serilog](https://serilog.net/)  
[SpecFlow LivingDoc](https://docs.specflow.org/projects/specflow-livingdoc)  
[Allure Reporting](https://docs.qameta.io/allure/)  
[Extent Reporting](https://extentreports.com/)  
[Docker](https://www.docker.com/)  

<br>

## Acknowledgements

Thanks to the creators and maintainers of the tools and frameworks used in this project.

