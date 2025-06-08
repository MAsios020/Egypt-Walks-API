# Egypt Walks API 🐪

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4)
![Architecture](https://img.shields.io/badge/Architecture-DDD%20%7C%20Layered-blue)
![Authentication](https://img.shields.io/badge/Auth-JWT-orange)

A robust and scalable Web API for managing walking trails in Egypt, built with **ASP.NET Core 8** and architected using **Domain-Driven Design (DDD)** principles. This project serves as a comprehensive example of modern backend development practices.

---

## 📜 About The Project

**Egypt Walks API** provides a complete backend solution for managing geographical regions and associated walking trails. It is designed with a clean, decoupled architecture to ensure maintainability and scalability. The API supports full CRUD operations, secure authentication, and advanced data querying features like filtering, sorting, and pagination.

---

## ✨ Key Features

- **Full CRUD Operations:** for managing **Regions** and **Walks**.
- **Secure Authentication:** Implemented using **ASP.NET Core Identity** and **JWT (JSON Web Tokens)** for secure, stateless API access.
- **Role-Based Authorization:** Endpoints are secured and accessible based on user roles (e.g., "Reader", "Writer").
- **Dynamic Data Querying:**
  - **Filtering:** on multiple properties (e.g., `filterOn=Name&filterQuery=Track`).
  - **Sorting:** by any property, in ascending or descending order (e.g., `sortBy=Name&isAscending=true`).
  - **Pagination:** to efficiently handle large datasets (e.g., `pageNumber=1&pageSize=10`).
- **Clean, Decoupled Architecture:** Follows **Domain-Driven Design** to separate business logic from infrastructure concerns.
- **Automated API Documentation:** Integrated **Swagger (OpenAPI)** for easy exploration and testing of all endpoints.

---

## 🏗️ Architectural & Design Approach

This project is architected using a layered approach inspired by **Domain-Driven Design (DDD)**. This methodology ensures a clean separation of concerns, making the application easier to test, maintain, and scale.

### Code Methodology: Domain-Driven Design (DDD)

The core of the application is the **Domain Model**, which is a rich representation of the business logic and rules. This model is completely isolated from infrastructure concerns, ensuring its purity and focus.

### Design Patterns Used

- **Repository Pattern:** Abstract a collection of domain objects. The `IRepository` interfaces are defined in the Domain layer, while their implementations, which handle data access using Entity Framework Core, reside in the Infrastructure layer.
- **Dependency Injection (DI):** Extensively used throughout the application to decouple components and facilitate testing. Services like Repositories, DbContexts, and Identity are injected where needed.
- **Data Transfer Object (DTO) Pattern:** Used to separate the internal domain models from the data structures exposed by the API. This prevents leaking domain logic and provides a stable API contract.
- **Options Pattern:** Used to bind configuration sections from `appsettings.json` (like JWT settings) to strongly-typed C# objects.

---

## 🛠️ Technologies, Frameworks, and Libraries

### Core Frameworks
- **.NET 8:** The underlying platform for building the application.
- **ASP.NET Core 8:** For building the web API.
- **Entity Framework Core 8:** The Object-Relational Mapper (O/RM) used for data access.

### Authentication & Security
- **ASP.NET Core Identity:** For managing users, roles, and password hashing.
- **JWT Bearer Authentication:** For securing API endpoints using JSON Web Tokens.

### Database
- **Microsoft SQL Server:** The relational database used to store application and authentication data.

### Libraries & Tools
- **AutoMapper:** For automatically mapping between Domain Model objects and DTOs.
- **Swashbuckle (Swagger):** For generating interactive API documentation.
- **Serilog (Optional, but recommended):** For structured logging capabilities.

---

## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- **.NET 8 SDK:** [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server:** An instance of SQL Server (e.g., Express, Developer, or LocalDB).
- **IDE:** Visual Studio 2022 or a code editor like VS Code.
- **Git:** For cloning the repository.


---

## 🔑 API Endpoints Guide

After running the project, the API documentation will be available at `/swagger`. This is the best way to test the endpoints interactively.

### Authentication Endpoints (`/api/Auth`)
- **`POST /Register`**
  - Registers a new user. Assigns "Reader" role by default.
  - **Body:** `{ "username": "string", "password": "string" }`
- **`POST /Login`**
  - Authenticates a user and returns a JWT token.
  - **Body:** `{ "username": "string", "password": "string" }`

### Regions Endpoints (`/api/Regions`)
- `GET /`: Get all regions.
- `GET /{id}`: Get a region by its ID.
- `POST /`: Create a new region. **(Requires "Writer" role)**
- `PUT /{id}`: Update an existing region. **(Requires "Writer" role)**
- `DELETE /{id}`: Delete a region. **(Requires "Writer" role)**

### Walks Endpoints (`/api/Walks`)
- **`GET /`**
  - Get all walks with support for filtering, sorting, and pagination.
  - **Query Parameters:**
    - `filterOn=Name&filterQuery=Giza`
    - `sortBy=LengthInKm&isAscending=false`
    - `pageNumber=1&pageSize=5`
- `GET /{id}`: Get a walk by its ID.
- `POST /`: Create a new walk. **(Requires "Writer" role)**
- `PUT /{id}`: Update an existing walk. **(Requires "Writer" role)**
- `DELETE /{id}`: Delete a walk. **(Requires "Writer" role)**

