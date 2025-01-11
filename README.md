## Objectives
Provide a RESTful API that allows CRUD operations (Create, Read, Update, Delete) on resources.

Use EF Core to interact efficiently with the database.

Implement user-secrets to store sensitive configurations, such as connection strings and API keys, thereby avoiding their exposure in the source code.

## Requirements
.NET SDK (version 9.0)

Visual Studio or any other IDE compatible with .NET

SQL Server or any other database provider compatible with EF Core

## Project Setup

1. **Clone the Repository**
   
   ```bash
   git clone https://github.com/JoseA-Cueto/Api-Rest-Minimal.git  
   cd ApiRestMinimal
   ```
2. **Restore dependencies**
   
   ```bash
   dotnet restore
   ```
3. **Migrate the database**
   
   ```bash
   dotnet ef database update
   ```
4. **Ejecutar la aplicación**
   
   ```bash
   dotnet run
   ```
