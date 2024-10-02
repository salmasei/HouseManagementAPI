House Management API
This is a simple House Management API project demonstrating three versions with progressive improvements in architecture and design, showcasing basic CRUD operations, synchronized caching, and implementation of CQRS (Command Query Responsibility Segregation) and clean code principles.

Branches Overview
The project contains three main branches, each focusing on different stages of improvements:

1. basic-crud
Description: This is the initial version of the project. It implements basic CRUD (Create, Read, Update, Delete) operations for house management.
Main Features:
Basic house management API with in-memory caching.
No synchronization between cache operations.
Technologies: ASP.NET Core, In-memory cache.
2. asynced-cache
Description: This version improves the initial CRUD operations by adding synchronized caching. The cache logic is abstracted into an interface and uses Dependency Injection for better manageability.
Main Features:
Cache synchronization is implemented to avoid race conditions.
Dependency Injection is used to handle the caching logic.
Technologies: ASP.NET Core, In-memory cache, Dependency Injection.
3. cqrs-clean-code
Description: This version refactors the project by introducing CQRS and clean code practices, ensuring separation between commands and queries.
Main Features:
CQRS architecture with command and query handlers.
Event notification service to log events and further decouple concerns.
Clean code principles applied to ensure scalability and testability.
Rate limiting has been added as an additional feature to control API traffic.
Technologies: ASP.NET Core, CQRS, Mediator pattern, Event Notification, Rate limiting.
Branch Structure
master: This branch holds the latest stable version of the project. All final improvements from different branches are merged here.
basic-crud: The starting point with basic CRUD operations.
asynced-cache: Enhanced version with cache synchronization and Dependency Injection.
cqrs-clean-code: Refactored version with CQRS implementation, notification services, and additional clean code practices.
How to Run the Project
Prerequisites
.NET SDK installed on your machine.
Visual Studio or any C# compatible IDE.
Running the Project Locally
Clone the Repository:

bash
Copy code
git clone https://github.com/your-username/HouseManagementAPI.git
Switch to a Branch:

bash
Copy code
git checkout <branch-name>
For example:

bash
Copy code
git checkout cqrs-clean-code
Restore Dependencies:

bash
Copy code
dotnet restore
Run the Application:

bash
Copy code
dotnet run
Testing the Project
Unit tests have been written for various parts of the API. To run the tests:

bash
Copy code
dotnet test
Folder Structure
bash
Copy code
HouseManagementAPI/
│
├── Caching/
│   └── InMemoryCacheService.cs      # Cache implementation
│
├── Controllers/
│   └── HouseController.cs           # API Controller for house management
│
├── CQRS/
│   ├── Commands/
│   ├── Handlers/
│   ├── Queries/
│
├── Models/
│   └── HouseModel.cs                # Data model for house
│
├── Notifications/
│   └── HouseNotificationService.cs  # Event notification service
│
├── Repositories/
│   └── IHouseCacheService.cs        # Cache repository interface
│
├── Validation/
│   └── HouseModelValidator.cs       # Validation logic for house models
│
├── Tests/
│   └── HouseServiceTests.cs         # Unit tests for the project
│
└── Program.cs                       # Main entry point
Screenshots
Branch Structure in Visual Studio

Solution Structure
