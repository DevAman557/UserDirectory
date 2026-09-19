# User Directory

A full-stack User Directory application built using **React**, **ASP.NET Core 8 Web API**, **Entity Framework Core**, and **SQLite**.

## Technologies

### Frontend
- React
- JavaScript
- React Router
- Vite
- CSS
- Vitest
- React Testing Library

### Backend
- .NET 8
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- xUnit
- Moq

## Application Structure

```text
UserDirectory
│
├── UserDirectory.Api
│   ├── Controllers
│   ├── Data
│   ├── DTOs
│   ├── Models
│   ├── Repositories
│   ├── Services
│   └── Migrations
│
├── UserDirectory.Api.Tests
│   └── UserServiceTests.cs
│
└── User-Directory-UI
    └── src
        ├── Pages
        ├── services
        ├── test
        ├── App.jsx
        └── App.css
```

## Architecture

The application follows a simple layered architecture:

```text
React UI
   ↓
ASP.NET Core Web API
   ↓
Controller
   ↓
Service
   ↓
Repository
   ↓
Entity Framework Core
   ↓
SQLite
```

### Layer Responsibilities

- **Controller** – Handles HTTP requests and responses.
- **Service** – Contains application and business logic.
- **Repository** – Handles database operations.
- **Entity Framework Core** – Provides ORM and database access.
- **SQLite** – Provides persistent data storage.

## Main Features

- Add and list users
- RESTful CRUD API
- SQLite persistence
- Client-side and server-side validation
- Loading, success, empty, and error states
- Swagger / OpenAPI
- Dependency Injection
- Async/Await
- Unit tests for frontend and backend

## Validation

- **Name:** Required, 2–100 characters
- **Age:** Integer between 0–120
- **City:** Required
- **State:** Required
- **Pincode:** Required, 4–10 characters

Validation errors are displayed inline on the Add User form.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/users` | Get all users |
| GET | `/api/users/{id}` | Get user by ID |
| POST | `/api/users` | Create a user |
| PUT | `/api/users/{id}` | Update a user |
| DELETE | `/api/users/{id}` | Delete a user |

## Development Process

1. Created the .NET 8 Web API and React application.
2. Designed the User model and DTOs.
3. Configured Entity Framework Core with SQLite.
4. Created and applied database migrations.
5. Implemented Controller, Service, and Repository layers.
6. Added CRUD API endpoints and validation.
7. Implemented React Add User and User List pages.
8. Connected the React application with the Web API.
9. Added loading, success, empty, and error handling.
10. Added frontend and backend unit tests.
11. Tested the complete React → API → EF Core → SQLite flow.

## Testing

**Backend:** 8 unit tests using xUnit and Moq.

**Frontend:** 3 tests using Vitest and React Testing Library.

**Total:** 11 automated tests.

## AI Assistance

AI tools were used as development assistance for:

- Exploring implementation approaches
- Code examples and debugging
- Test case development
- Code and documentation review

All generated code was reviewed, adapted, and tested before inclusion in the project.

## Bonus Features

The following optional bonus features were not implemented:

- OAuth2 / OpenID Connect authentication
- Docker / Docker Compose

The core assignment requirements are implemented.

## Run Locally

### Backend

```bash
cd UserDirectory.Api
dotnet restore
dotnet run
```

### Frontend

```bash
cd User-Directory-UI
npm install
npm run dev
```

The React application normally runs at:

```text
http://localhost:5173
```

## Repository

**GitHub:** https://github.com/DevAman557/UserDirectory

## License

This project was developed as part of a coding assignment.
