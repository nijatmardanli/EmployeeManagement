# Employee Management API

This project provides a set of APIs to manage employees and departments. The project is built with ASP.NET Core and uses Docker for containerization. SQL Server is used as the database, and Redis is used for caching.

## Getting Started

### Prerequisites

- Docker
- .NET 6.0 SDK

### Running the Project

You can run the project either through Visual Studio or by using Docker Compose.

## API Endpoints

### Department Controller

- **GET /api/department/{id}**
- Get department by ID
- **GET /api/department**
- Get all departments
- **GET /api/department/Filter**
- Filter departments
- **POST /api/department**
- Add a new department
- **PUT /api/department**
- Update an existing department
- **DELETE /api/department/{id}**
- Delete department by ID

### Employee Controller

- **GET /api/employee/{id}**
- Get employee by ID
- **GET /api/employee**
- Get all employees
- **GET /api/employee/Filter**
- Filter employees
- **POST /api/employee**
- Add a new employee
- **PUT /api/employee**
- Update an existing employee
- **DELETE /api/employee/{id}**
- Delete employee by ID

## Technologies Used

- ASP.NET Core
- Docker
- SQL Server
- Redis
