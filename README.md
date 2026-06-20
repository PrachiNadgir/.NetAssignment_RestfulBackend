# Product Management API

## Overview

Product Management API is a RESTful Web API built using ASP.NET Core 8 following Clean Architecture principles. The application provides secure product management functionality with JWT Authentication, Refresh Tokens, Entity Framework Core, SQL Server, Docker support, Logging, Validation, and Unit Testing.

## Architecture

The solution follows Clean Architecture and is divided into four layers:

text
ProductManagementAPI
│
├── API
│   ├── Controllers
│   ├── Middleware
│   ├── Filters
│   ├── Extensions
│
├── Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   ├── Validators
│   └── Mapping
│
├── Domain
│   ├── Entities
│   ├── Events
│   ├── Exceptions
│   └── Enums
│
├── Infrastructure
│   ├── Data
│   ├── Repositories
│   ├── Identity
│   ├── Logging
│   └── Services
│
└── Tests

## Features

* ASP.NET Core 8 Web API
* Clean Architecture
* Entity Framework Core
* SQL Server
* Repository Pattern
* Unit of Work Pattern
* JWT Authentication
* Refresh Token Support
* AutoMapper
* FluentValidation
* Swagger Documentation
* Serilog Logging
* Global Exception Handling
* Docker Support
* Unit Testing (xUnit + Moq)

## Technologies Used

| Technology            | Version |
|  | - |
| ASP.NET Core          | 8.0     |
| Entity Framework Core | 8.0     |
| SQL Server            | 2022    |
| JWT Authentication    | Latest  |
| AutoMapper            | Latest  |
| FluentValidation      | Latest  |
| Serilog               | Latest  |
| Swagger               | Latest  |
| Docker                | Latest  |
| xUnit                 | Latest  |


## Authentication

### Login

Endpoint:

http
POST /api/auth/login

Request:

json
{
  "username": "admin",
  "password": "admin123"
}


Response:

json
{
  "accessToken": "jwt_token",
  "refreshToken": "refresh_token"
}


### Refresh Token

Endpoint:

http
POST /api/auth/refresh


Request:

json
{
  "refreshToken": "refresh_token"
}

Response:

json
{
  "accessToken": "new_jwt_token",
  "refreshToken": "new_refresh_token"
}

## Product Endpoints

### Get All Products

http
GET /api/product


### Get Product By Id

http
GET /api/product/{id}


### Create Product

http
POST /api/product


Request:

json
{
  "productName": "Laptop"
}

### Update Product

http
PUT /api/product/{id}

Request:

json
{
  "productName": "Gaming Laptop"
}

### Delete Product

http
DELETE /api/product/{id}

## Database Configuration

Example connection string:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True"
}


For Docker:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=sqlserver;Database=ProductDB;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;Encrypt=False"
}

## Running the Application

### Clone Repository

bash
git clone <repository-url>


bash
cd ProductManagementAPI

### Restore Packages

bash
dotnet restore

### Apply Migrations

bash
dotnet ef database update

### Run Application

bash
dotnet run


Swagger UI:

text
http://localhost:5000/swagger


## Docker Setup

### Build Containers

bash
docker compose build


### Run Containers

bash
docker compose up


Swagger:

text
http://localhost:5000/swagger


## Logging

Serilog is configured for:

* Console Logging
* File Logging

Log files are stored in:

text
Logs/

## Validation

FluentValidation is used for request validation.

Example:

csharp
RuleFor(x => x.ProductName)
    .NotEmpty()
    .MaximumLength(255);

## Testing

Run all tests:

bash
dotnet test


Testing Frameworks:

* xUnit
* Moq
* FluentAssertions

## Design Patterns Used

* Clean Architecture
* Repository Pattern
* Unit Of Work Pattern
* Dependency Injection
* DTO Pattern
* Middleware Pattern
