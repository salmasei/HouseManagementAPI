# House Management System API

## Overview

This repository contains a simple house management system API developed in three versions:
1. **Basic CRUD**: Implements simple Create, Read, Update, Delete operations.
2. **Async with Cache**: Refactored controller methods to use asynchronous calls for handling I/O operations and multiple concurrent requests more efficiently.
3. **CQRS with Clean Code**: Refactors the project using the CQRS (Command Query Responsibility Segregation) pattern and follows clean code principles.

Each version demonstrates improvements in architecture and performance.

## Branches

- `basic-crud`: Contains the initial version with basic CRUD operations.
- `asynced-cache`: Asynchronous calls for handling I/O.
- `cqrs-clean-code`: Refactors the system to implement CQRS pattern and follow clean code principles.

## Services

### House Management API

- **Fields**: `address`, `numberOfFloors`, `unitType`, `features`
- **Endpoints**:
  - `GET /House`
  - `POST /House`
  - `PUT /House/{address}`
  - `DELETE /House/{address}`

## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Running the Application

1. **Clone the repository:**
    ```bash
    git clone https://github.com/salmasei/HouseManagementAPI.git
    cd HouseManagementAPI
    ```

2. **Switch to the desired branch**:
    - For the basic CRUD version:
        ```bash
        git checkout basic-crud
        ```
    - For the async with cache version:
        ```bash
        git checkout asynced-cache
        ```
    - For the CQRS clean code version:
        ```bash
        git checkout cqrs-clean-code
        ```

3. **Build the Solution:**
    Open the solution in Visual Studio or use the following command in the terminal:
    ```bash
    dotnet build
    ```

4. **Run the Application:**
    ```bash
    dotnet run
    ```

5. **Run the Tests:**
    ```bash
    dotnet test
    ```

## Usage Instructions

1. **Start the API:**
    After running the application, navigate to `https://localhost:{port}/swagger/index.html` to view the API documentation and available endpoints using Swagger UI.

2. **Create a House:**
    - Use the `POST /House` endpoint to create a new house.
    - Provide details like `address`, `numberOfFloors`, `unitType` and `features`.

3. **Update a House:**
    - Use the `PUT /House/{id}` endpoint to update house information.

4. **Delete a House:**
    - Use the `DELETE /House/{id}` endpoint to remove a house from the system.

## Project Structure

- `HouseManagementAPI`: Contains the API implementation with different patterns (CRUD, Async, CQRS).
- `HouseManagementAPITests`: Contains unit tests for the API functionalities.

## Contact

For any questions or issues, please contact [seifvand@gmail.com](mailto:seifvand@gmail.com).
