# User Directory

A full-stack User Directory application developed as a coding assignment using **React**, **ASP.NET Core 8 Web API**, **Entity Framework Core**, and **SQLite**.

The application allows users to be added and displayed through a simple React UI, with persistent data storage using SQLite.

---

## Features

- Add a new user
- View all users
- RESTful CRUD API
- Client-side form validation
- Server-side validation using Data Annotations
- Inline validation messages
- Loading state
- Empty state
- Error handling and retry
- Success message after creating a user
- SQLite database persistence
- Swagger / OpenAPI documentation
- Dependency Injection
- Async/Await
- Repository and Service layers
- Backend and frontend unit tests

---

## Technology Stack

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

---

## Architecture

The application follows a simple layered architecture:

```text
                    React SPA
                       |
                       | HTTP / REST API
                       v
              ASP.NET Core Web API
                       |
                       v
                   Controller
                       |
                       v
                    Service
                       |
                       v
                  Repository
                       |
                       v
             Entity Framework Core
                       |
                       v
                  SQLite DB
