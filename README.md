# FinanceManager

## Overview
FinanceManager is a modular, extensible WPF desktop application built with clean architectural principles and modern development patterns. It provides user registration, login with JWT-based authentication, and a foundation for financial data management — all through a responsive, Material Design-based UI.

This project serves as both a scalable foundation and a practical sample for building real-world enterprise desktop applications with a layered architecture.

---

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

---

## Technologies Used

### Backend (API)

| Technology | Purpose |
|------------|---------|
| **.NET 8** | Runtime framework |
| **ASP.NET Core** | Web API framework |
| **Entity Framework Core** | ORM for data access |
| **SQL Server** | Database |
| **JWT Authentication** | Secure token-based auth |
| **FluentValidation** | Input validation |
| **AutoMapper** | Object mapping |
| **Newtonsoft.Json** | JSON serialization |
| **Swagger/OpenAPI** | API documentation |

### Frontend (Client)

| Technology | Purpose |
|------------|---------|
| **WPF (.NET 8)** | Desktop UI framework |
| **Prism.Wpf** | UI architecture |
| **HttpClient** | HTTP communication |
| **FluentValidation** | Input validation |
| **AutoMapper** | Object mapping |
| **MaterialDesignThemes** | Material Design UI components |
| **RestSharp** | REST API client |
| **Dependency Injection** | IoC container |

### Design Patterns

- **MVVM Pattern**: Full Model-View-ViewModel implementation in WPF
- **Repository Pattern**: Data access abstraction with generic base repository
- **Factory Pattern**: Service and View factories for flexible instantiation
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection throughout
- **SOLID Principles**: Single responsibility, interface segregation, dependency inversion
- **Clean Architecture**: Separation of concerns with layered approach

---

## Project Structure
```
FinanceManager/
├── FinanceManager.API/          # ASP.NET Core Web API
│   ├── Application/             # Business logic & interfaces
│   │   ├── Services/            # Business services
│   │   ├── Validation/          # FluentValidation validators
│   │   ├── Authorization/       # Custom authorization handlers
│   │   └── Mapping/             # AutoMapper profiles
│   ├── Domain/                  # Entities & business models
│   │   └── Entities/            # User, Transaction entities
│   ├── Infrastructure/          # External dependencies
│   │   ├── Context/             # Entity Framework DbContext
│   │   ├── Repositories/        # Data access layer
│   │   ├── Authentication/      # JWT token generation
│   │   └── Middlewares/         # Exception & JWT middleware
│   └── Presentation/            # API Controllers
│
├── FinanceManager.WPF/          # WPF Desktop Application
│   ├── Application/             # Application services
│   │   ├── Services/            # UI service implementations
│   │   ├── Validation/          # Client-side validators
│   │   └── Mapping/             # AutoMapper profiles
│   ├── Domain/                  # Client-side models
│   ├── Infrastructure/          # Infrastructure concerns
│   │   └── Connectors/          # REST API connectors
│   ├── Presentation/            # MVVM components
│   │   ├── ViewModels/          # View models with validation
│   │   ├── Views/               # XAML views
│   │   └── Interfaces/          # Abstraction contracts
│   └── Extensions/              # Dependency injection setup
│
└── FinanceManager.Shared/       # Shared library
    ├── Application/             # DTOs, Requests, Responses
    ├── Enums/                   # UserRole, TransactionType
    ├── Exceptions/              # Custom exception hierarchy
    ├── Constants/               # Application constants
    └── Helpers/                 # JSON serialization helpers
```

---

## Authentication Flow
- JWT token is issued at login and stored in memory using a session manager.
- Token is automatically injected into every authorized request via UI service layer.
- APIs are protected with `[Authorize(Policy = ...)]` using ASP.NET Core Authorization..
- A custom `IAuthorizationMiddlewareResultHandler` returns uniform JSON messages for unauthorized access

---

## API Endpoints
| Method    | Endpoint                   | Description                                          | Authorization Policy |
|-----------|----------------------------|------------------------------------------------------|----------------------|
| POST      | /api/auth/login            | Authenticate the user and generates the Access Token | N/A                  |
| GET       | /api/users                 | Fetch all users registered in the system             | AdminPolicy          |
| POST      | /api/users/register        | Register new user in the system                      | N/A                  |
| GET       | /api/transactions/{userId} | Fetch all transactions of a specific user            | StandardPolicy       |

---

## Error Handling Strategy
- API errors return consistent `ErrorResponse` models with messages and status codes.
- ViewModels react to failed requests by setting `ErrorMessage` to a user-readable value.
- FluentValidation gives real-time form feedback via `INotifyDataErrorInfo`.
- Logging captures client and server errors via injected `ILogger<T>`.

---

## Installation
### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)
- Windows 10/11 (for WPF application)

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/andreavallati/FinanceManager.git
   cd FinanceManager
   ```

2. **Configure the database connection**
   
   Update `appsettings.json` in `FinanceManager.API`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FinanceManagerDb;Trusted_Connection=true;"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd FinanceManager.API
   dotnet ef database update
   ```

4. **Build the solution**
   ```bash
   dotnet build
   ```

5. **Run the API**
   ```bash
   cd FinanceManager.API
   dotnet run
   ```
   The API will be available at `https://localhost:7258`

6. **Run the WPF application**
   
   In a separate terminal:
   ```bash
   cd FinanceManager.WPF
   dotnet run
   ```

---

<div align="center">

**Happy Coding!**

</div>
