# Distance Calculator

This is a solution to task for calculating distance. It follows the principles of Clean Architecture.


## Technologies

* [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 3.1](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [Xunit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)
* [Shoudly](https://github.com/shouldly/shouldly), [GuardClauses](https://github.com/ardalis/GuardClauses)
* [Docker](https://www.docker.com/)

## Getting Started

The easiest way to get started:

1. Install the latest [Docker](https://docs.docker.com/get-docker/)
2. Navigate into the solution folder using terminal/cmd
3. Run `docker-compose -f docker-compose.yml up --build -d` to setup and start the project
4. Run `docker ps` to verify everything is working
5. Go to `http://localhost:5000/swagger` to see the exposed api

Second option:
1. Install the latest [.NET 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)
3. Navigate to `src/Startup` and run `dotnet build` to build the project
4. Run `dotnet run --launch-profile Startup` to launch the back end (ASP.NET Core Web API)
5. Open browser and navigate to `http://localhost:5000/swagger`

*This option is configured to use InMemoryDatabase, no sql server is required


## Overview

<img src="https://user-images.githubusercontent.com/10745635/142755772-d90b2d60-29f6-46f1-a756-d974d9ea255e.png" width="400">

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Web

This layer is a presentation layer and ASP.NET Core 3.1 API. This layer depends on both the Application layer.

### Startup

This is the startup entrypoint where the app is configured. This layer depends on both the Application and Infrastructure layer to setup DI.

### Learn about Clean Architecture

[Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## Database Configuration

The template is configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

If you would like to use SQL Server, you will need to update **Startup/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

## License

This project is licensed with the [MIT license](LICENSE).
