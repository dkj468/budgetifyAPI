# budgetifyAPI - A .NET Core Web API for personal Budget Management

## Getting Started
To use this project template, follow the steps below:

 1. Ensure that you have the .NET 9 SDK installed on your machine.
2. Clone or download this repository to your local machine.
3. Open the solution in your preferred IDE (e.g., Visual Studio, Visual Studio Code).
4. Build the solution to restore NuGet packages and compile the code.
5. Configure the necessary database connection settings in the dotnet user-secrets.

## configure database connection string and JWT signing key
### development mode
For development mode, this project stores secrets like database connection string and JWT signing key using dotnet user-secrets. 
**database connection string**

    dotnet user-secrets set "dbconnection" "your connection string"
**JWT signing key**

    dotnet user-secrets set "JwtTokenKey" "your token key"
Read more about dotnet user-secrets here: [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows)

### production mode
For production mode, this project uses Azure key vault service to store secrets.
Read more about using Azure key vault service to store application secrets and use them by configuring managed identity: [Tutorial: Use a managed identity to connect Key Vault to an Azure web app in .NET](https://learn.microsoft.com/en-us/azure/key-vault/general/tutorial-net-create-vault-azure-web-app?tabs=azure-cli)

## 🚀 Features

This project follows best practices for developing a robust and scalable **.NET Core Web API**. It includes:

✅ **Entity Framework Core** – Code-first approach for database management  
✅ **Fluent Validation** – Centralized and maintainable request validation  
✅ **Repository Pattern** – Decouples data access logic from business logic  
✅ **JWT Authentication** – Secure user authentication and authorization  
✅ **Middleware for Exception Handling** – Standardized error responses  
✅ **Configuration via dotnet user-secrets & Azure key vault service** – Secure and flexible settings management  
✅ **Dependency Injection** – Built-in DI for better testability and maintainability  
✅ **Asynchronous Programming with async/await** – Improves performance  
✅ **Swagger** – API documentation and testing

## 🛠️ Future Development Roadmap

We are actively working on improving this project! Here’s what we plan to add in future versions:

🔹 **Implement Clean Architecture** – Enhance maintainability by adopting the clean architecture approach.  
🔹 **Introduce Microservices** – Break monolithic structure into modular, scalable microservices.  
🔹 **Implement CQRS Pattern** – Separate read and write operations for better performance.  
🔹 **GraphQL Support** – Provide an alternative to REST for more flexible queries.  
🔹 **Docker & Kubernetes Support** – Improve deployment and scalability using containerization.  
🔹 **gRPC Implementation** – Explore the use of gRPC for high-performance API calls.  
🔹 **Unit & Integration Testing** – Increase code reliability with xUnit, Moq, and test automation.  
🔹 **CI/CD Pipeline with GitHub Actions** – Automate testing and deployment workflows.
