# Clean Architecture Web API Template
This repository contains code which can be used as a template or starter-project for a new Web API project, complete with some best-practices and Clean (Onion) Architecture.
## Technologies used
- .NET 6 WebApi
- .NET 6 (class) libraries
- Clean (Onion) Architecture
- Mediator/CQRS Pattern (using [MediatR](https://github.com/jbogard/MediatR))
- Specification Pattern (using [Specification](https://github.com/ardalis/Specification))
- [Entity Framework Core](https://github.com/dotnet/efcore)
- [API Versioning](https://github.com/dotnet/aspnet-api-versioning)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- Generic Exception handling middleware

## Architecture Overview
![Onion Architecture](https://camo.githubusercontent.com/0e59a9d03c2d24ff031588265fdc00f6ccb8248f/68747470733a2f2f7777772e636f6465776974686d756b6573682e636f6d2f77702d636f6e74656e742f75706c6f6164732f323032302f30362f4f6e696f6e2d4172636869746563747572652d496e2d4153502e4e45542d436f72652e706e67)

This application uses the Clean (Onion) Architecture. Clean Architecture leverages the principles of Dependency Inversion as well as Domain-Driven-Design. It puts the business logic and application model at the center of the application. Instead of having business logic depend on data access or other infrastructure concerns, this dependency is inverted: infrastructure and implementation details depend on the Application Core. This is achieved by defining abstractions, or interfaces, in the Application Core, which are then implemented by types defined in the Infrastructure layer.

In the diagram above, dependencies flow toward the innermost circle. The Application Core takes its name from its position at the core of the diagram. As shown in the diagram, you can see that the Application Core has no dependencies on other application layers. Entities and interfaces are at the very center. Outside of the Application Core, both the Presentation and Infrastructure layers depend on the Application Core, but not on one another (necessarily).

### Organizing code in Clean (Onion) Architecture

In a Clean Architecture solution, each project has clear responsibilities. As such, certain types belong in each project and you'll frequently find folders corresponding to these types in the appropriate project.

#### Application Core

- Entities (business model classes that are persisted)
- Interfaces
- Services
- DTOs

#### Infrastructure
- EF Core types (Contexts and/or Migrations)
- Data Access implementation types (Repositories)
- Infrastructure-specific services (for example, EMailService, FileSystemService)

#### UI Layer
- Controllers
- Filters
- Views
- ViewModels
- Startup