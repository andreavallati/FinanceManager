# FinanceManager

## Overview
FinanceManager is a modular, extensible WPF desktop application built with clean architectural principles and modern development patterns. It provides user registration, login with JWT-based authentication, and a foundation for financial data management — all through a responsive, Material Design-based UI.

This project serves as both a scalable foundation and a practical sample for building real-world enterprise desktop applications with a layered architecture.

## Features
- Follows Clean Architecture principles
- Modular structure with clear separation of concerns between ViewModels, Services, Models, and external components
- Client application using WPF, MVVM pattern, and responsive, modern UI using MaterialDesignInXAML Toolkit.
- User registration mechanism with role assignment, form validation, and feedback.
- Secure login with JWT token generation, token handling and usage across UI service layers.
- Custom authorization policies and handlers for role-based API access.
- In-memory session management for authenticated users.
- RESTful API integration. Implementation of asynchronous communication using RestSharp, with model mapping via AutoMapper.
- Entity Framework for database management
- Robust validation logic with FluentValidation and INotifyDataErrorInfo, with real-time UI error feedback.
- Graceful error handling across both API and client layers.
- Dependency injection for service management

## Technologies Used
- .NET Core
- Entity Framework Core
- ASP.NET Core Web API
- ASP.NET Core Authorization for user roles management
- WPF with MVVM Pattern and MaterialDesignInXAML Toolkit for Client
- RestSharp for APIs consuming
- AutoMapper for automatic model mapping
- System.Text.Json for Serialization / Deserialization
- FluentValidation and INotifyDataErrorInfo for validation handling

## Project Structure
```
FinanceManager/
├── FinanceManager.Api/                      # API Layer
│   ├── Application/                         # Business logic, authorization, mappings and validation
│   ├── Domain/                              # Core domain entities
│   ├── Extensions/                          # Extension methods
│   ├── Infrastructure/                      # Data access, authentication, middlewares and repositories
│   ├── Presentation/                        # Controllers and endpoints
├── FinanceManager.WPF/                      # Client application (if applicable)
│   ├── Application/                         # Business logic, mappings and validation
│   ├── Converters/                          # WPF converters
│   ├── Domain/                              # Client models
│   ├── Extensions/                          # Extension methods
│   ├── Infrastructure/                      # API communication layer
│   ├── Presentation/                        # WPF Views and ViewModels
│   ├── Resources/                           # UI common resources
│   ├── ViewResources/                       # UI common styles
├── FinanceManager.Shared/                   # Shared utilities and models
│   ├── Application/                         # DTOs, POCO models and configuration settings
│   ├── Constants/                           # Constants
│   ├── Enums/                               # Enumerations
│   ├── Exceptions/                          # Custom exceptions
│   ├── Extensions/                          # Extension methods
│   ├── Helpers/                             # Common utility classes
```

## Authentication Flow
- JWT token is issued at login and stored in memory using a session manager.
- Token is automatically injected into every authorized request via UI service layer.
- APIs are protected with `[Authorize(Policy = ...)]` using ASP.NET Core Authorization..
- A custom `IAuthorizationMiddlewareResultHandler` returns uniform JSON messages for unauthorized access

## API Endpoints
| Method    | Endpoint                   | Description                                          | Authorization Policy |
|-----------|----------------------------|------------------------------------------------------|----------------------|
| POST      | /api/auth/login            | Authenticate the user and generates the Access Token | N/A                  |
| GET       | /api/users                 | Fetch all users registered in the system             | AdminPolicy          |
| POST      | /api/users/register        | Register new user in the system                      | N/A                  |
| GET       | /api/transactions/{userId} | Fetch all transactions of a specific user            | StandardPolicy       |

## Error Handling Strategy
- API errors return consistent `ErrorResponse` models with messages and status codes.
- ViewModels react to failed requests by setting `ErrorMessage` to a user-readable value.
- FluentValidation gives real-time form feedback via `INotifyDataErrorInfo`.
- Logging captures client and server errors via injected `ILogger<T>`.

## Installation
### Prerequisites
- .NET SDK 6.0 or later
- SQL Server (if using database authentication)

### Steps
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd FinanceManager
   ```
2. Install dependencies:
   ```sh
   dotnet restore
   ```
3. Modify `appsettings.json` to set a ClientSecret for JWT authentication and the correct connection string for SQL Server:
   ```sh
   "DefaultConnection": "Server=(localdb)\\YourInstance;Database=YourDatabase;Trusted_Connection=True;"
   "ClientSecret": "YourClientSecret"
   ```
4. Initialize EF database:
   ```sh
   Add-Migration First migration
   Update-Database
   ```
   
## Usage
1. Run the API project:
   ```sh
   dotnet run --project FinanceManager.Api
   ```
2. Run the client application in Visual Studio.
