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
